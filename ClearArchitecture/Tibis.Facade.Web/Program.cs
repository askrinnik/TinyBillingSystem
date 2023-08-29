
using Tibis.Application.AccountManagement.Services;
using Tibis.Application.Billing.Services;
using Tibis.Application.ProductManagement.Services;

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

        builder.Services.AddHttpClient<IProductClient, ProductHttpClient>();
        builder.Services.AddHttpClient<IAccountClient, AccountHttpClient>();
        builder.Services.AddHttpClient<IBillingClient, BillingHttpClient>();
        builder.Services.AddSingleton<IFacadeService, FacadeService>();

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