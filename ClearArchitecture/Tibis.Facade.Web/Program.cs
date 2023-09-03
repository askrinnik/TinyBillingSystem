using LokiLoggingProvider.Options;
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

        builder.Logging.AddLoki(configure =>
        {
            configure.Client = PushClient.Grpc;
            configure.StaticLabels.JobName = "Tibis.Facade";
            configure.StaticLabels.AdditionalStaticLabels.Add("SystemName", "Tibis");
            configure.Formatter = Formatter.Json;
        });

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