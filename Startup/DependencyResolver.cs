using Paychex_SimpleTimeClock.Authorization.Interface;
using Paychex_SimpleTimeClock.Authorization.Repository;
using Paychex_SimpleTimeClock.DataAccess.Interface;
using Paychex_SimpleTimeClock.DataAccess.Repository;

namespace Paychex_SimpleTimeClock.Startup
{
    public static class DependencyResolver
    {
        public static void AddDependencies(this IServiceCollection serviceCollection)
        {
            //Authorization
            serviceCollection.AddTransient<IPermissionProvider, PermissionProvider>();

            //DataAccess            
            serviceCollection.AddTransient<IPaychexDataAccess, PaychexDataAccess>();

            serviceCollection.AddTransient<IDataSeeder, DataSeeder>();

            //Factories            
            //serviceCollection.AddTransient<IInvestorReportingEmail, InvestorReportingEmail>();

            //Services             
            //serviceCollection.AddTransient<IConfigService, ConfigService>();
        }
    }
}
