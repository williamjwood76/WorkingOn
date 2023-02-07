using Paychex_SimpleTimeClock.DatabaseObjects;

namespace Paychex_SimpleTimeClock.DataAccess.Interface
{
    public interface IPaychexDataAccess
    {
        public Task<IEnumerable<AvailableBreaks>> GetAvailableBreaks();

        public Task<string> Login(string username, string password);
    }
}
