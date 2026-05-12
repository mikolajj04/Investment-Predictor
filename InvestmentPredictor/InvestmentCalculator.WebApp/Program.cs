using Microsoft.EntityFrameworkCore;
using ApexCharts;
using InvestmentPredictor.Core;
using InvestmentCalculator.WebApp.Data;
using InvestmentCalculator.WebApp.Components;
using InvestmentCalculator.WebApp.Services;


namespace InvestmentPredictor
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddApexCharts();
            builder.Services.AddScoped<IInvestmentCalculator, InvestmentPredictor.Core.InvestmentCalculator>();
            builder.Services.AddHttpClient<IMarketNewsService, MarketNewsService>(client=>
            {
                client.BaseAddress = new Uri("https://www.alphavantage.co/");
            });
            var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
            var supportedCultures = new[] { "pl-PL" };
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);
            app.Run();
        }
    }
}
