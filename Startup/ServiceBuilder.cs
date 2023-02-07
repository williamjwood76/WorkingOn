using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Paychex_SimpleTimeClock.DatabaseObjects;

namespace Paychex_SimpleTimeClock.Startup
{
    public class ServiceBuilder
    {
        public async Task<WebApplication> AddServices(WebApplicationBuilder serviceBuilder)
        {
            var services = serviceBuilder.Services;

            services
                .AddControllersWithViews(options =>
                {
                    options.AllowEmptyInputInBodyModelBinding = true;
                    options.RespectBrowserAcceptHeader = true;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                });

            services.AddDbContextFactory<PaychexApplicationsDB>(options =>
            {
                options.UseInMemoryDatabase("PaychexTestDB");
            });

            services.AddBundles();

            services.AddDependencies();

            services.AddHttpContextAccessor();
            services.AddSession();

            services.AddHttpClient();

            return serviceBuilder.Build();
        }
    }
}
