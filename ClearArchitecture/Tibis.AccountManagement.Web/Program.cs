using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Tibis.AccountManagement.Application;
using Tibis.AccountManagement.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddHandlers();

const string serviceName = "Tibis.AccountManagement";

var openTelemetryBuilder = builder.Services.AddOpenTelemetry() // add the OpenTelemetry.Extensions.Hosting nuget package
    .ConfigureResource(resource => resource.AddService(serviceName));

openTelemetryBuilder
    .WithTracing(tracerProviderBuilder =>
            tracerProviderBuilder
                .AddSource(serviceName)
                .AddAspNetCoreInstrumentation(options => options.RecordException = true) // add the pre-release OpenTelemetry.Instrumentation.AspNetCore nuget package
                .AddHttpClientInstrumentation() // add the pre-release OpenTelemetry.Instrumentation.Http nuget package
                .AddOtlpExporter() // 4317 // add the OpenTelemetry.Exporter.OpenTelemetryProtocol nuget package
    );

var app = builder.Build();

// Use ErrorsController to handle errors
app.UseExceptionHandler(new ExceptionHandlerOptions { ExceptionHandlingPath = "/error", AllowStatusCode404Response = true });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
