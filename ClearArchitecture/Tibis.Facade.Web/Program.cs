using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Tibis.AccountManagement.HttpClients;
using Tibis.Billing.HttpClients;
using Tibis.ProductManagement.HttpClients;

namespace Tibis.Facade.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAccountManagementHandlers();
        builder.Services.AddProductManagementHandlers();
        builder.Services.AddBillingHandlers();

        builder.Services.AddSingleton<IFacadeService, FacadeService>();

        const string serviceName = "Tibis.Facade";

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

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}