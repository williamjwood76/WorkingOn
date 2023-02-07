using Microsoft.EntityFrameworkCore;
using Paychex_SimpleTimeClock.DataAccess.Interface;
using Paychex_SimpleTimeClock.DatabaseObjects;
using Paychex_SimpleTimeClock.Models;

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

        public async Task<List<TimeClockModel>> GetAvailableWorkShiftsByUser(Guid UserID)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            return
                (
                from us in dbContext.UserShifts
                join u in dbContext.Users on us.UserID equals u.UserID
                select new TimeClockModel
                {
                    userShifts = us,
                    users = u
                })
                .ToListAsync()
                ;
                //.Join(dbContext.Users,
                //u => u.UserID,
                //p =>p.UserID,
                //(u, p) => new 
                //{
                //    p.UserID,
                //    p.UserRoleID,
                //    p.UserName,
                //    u.AvailableShiftID,
                //    u.UserShiftStart,
                //    u.UserShiftEnd,

                //})
                //.Where(x => x.Active, y => y.UserID == UserID)
                //.OrderByDescending(o => o.UserShiftStart)
                //.ToListAsync();
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
