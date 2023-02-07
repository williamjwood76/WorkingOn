namespace Paychex_SimpleTimeClock.Startup
{
    public interface IDataSeeder
    {
        public Task LoadTablesIntoMemory(IServiceProvider serviceProvider);
    }
}
