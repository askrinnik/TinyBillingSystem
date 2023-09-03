using LokiLoggingProvider.Options;
using Tibis.ProductManagement.Application;
using Tibis.ProductManagement.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddHandlers();

builder.Logging.AddLoki(configure =>
{
    configure.Client = PushClient.Grpc;
    configure.StaticLabels.JobName = "Tibis.ProductManagement";
    configure.StaticLabels.AdditionalStaticLabels.Add("SystemName", "Tibis");
    configure.Formatter = Formatter.Json;
});

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
