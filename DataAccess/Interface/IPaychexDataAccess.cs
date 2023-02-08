using Paychex_SimpleTimeClock.DatabaseObjects;

namespace Paychex_SimpleTimeClock.DataAccess.Interface
{
    public interface IPaychexDataAccess
    {
        public Task<IEnumerable<AvailableBreaks>> GetAvailableBreaks(int userId);

        public Task<int> Login(string username, string password);

        public Task<Users?> GetLoggedInUser(int userID);

        public Task<IEnumerable<object>> GetAvailableWorkShiftsByUser(int userId);

        public Task<int> AddUserStartTime(int userId);


    }
}
