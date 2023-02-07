using Microsoft.EntityFrameworkCore;
using Paychex_SimpleTimeClock.DataAccess.Interface;
using Paychex_SimpleTimeClock.DatabaseObjects;

namespace Paychex_SimpleTimeClock.DataAccess.Repository
{
    public class PaychexDataAccess : IPaychexDataAccess
    {
        private readonly IDbContextFactory<PaychexApplicationsDB> _dbContextFactory;
        
        public PaychexDataAccess(IDbContextFactory<PaychexApplicationsDB> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<string> Login(string username, string password)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            var v = dbContext.Users;
            return await dbContext.Users
                .Where(x => x.UserName == username)
                .Where(x => x.Password == password)
                .Select(x => x.UserID)
                .FirstOrDefaultAsync()
                ;

        }


        public async Task<IEnumerable<AvailableBreaks>> GetAvailableBreaks()
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            return await dbContext.AvailableBreaks
                .Where(x => x.Active)
                .ToListAsync();

        }

        public async Task<Users?> GetLoggedInUser(string userID)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            return await dbContext.Users
                .Where(x => x.UserID == userID)
                .FirstOrDefaultAsync();
        }


    }
}
