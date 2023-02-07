using Paychex_SimpleTimeClock.DataAccess.Interface;
using Paychex_SimpleTimeClock.DataAccess.Repository;

namespace Paychex_SimpleTimeClock.Startup
{
    public static class DependencyResolver
    {
        public static void AddDependencies(this IServiceCollection serviceCollection)
        {
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
