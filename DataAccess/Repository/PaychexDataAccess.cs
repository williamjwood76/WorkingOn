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
        public async Task<int> Login(string username, string password)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            var v = dbContext.Users;
            return await dbContext.Users
                .Where(x => x.UserName == username)
                .Where(x => x.Password == password)
                .Select(x => x.UserID)
                .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<AvailableBreaks>> GetAvailableBreaks(int userId)
        {
            if (!await IsClockedIn(userId))
                return null;

            //have taken any breaks?




            //await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            //return await dbContext.AvailableBreaks
            //    .Where(x => x.Active)
            //    .Where(x => x.AvailableShiftID)
            //    .ToListAsync();
            return null;
        }

        public async Task<IEnumerable<object>> GetAvailableWorkShiftsByUser(int userId)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            var results = await
                (
                    from userShifts in dbContext.UserShifts
                    join userBreaks in dbContext.UserBreaks on userShifts.UserID equals userId
                    join user in dbContext.Users on userBreaks.UserID equals userId
                    where 
                        user.UserID == userId
                        &&
                        user.Active
                    select new 
                    {
                        user.UserID,
                        user.UserRoleID,
                        user.UserName,
                        userShifts.AvailableShiftID,
                        userShifts.UserShiftStart,
                        userShifts.UserShiftEnd
                        //add unique id field here
                    }
                ).ToListAsync();
            //Todo:remove it here.
            await dbContext.CustomSaveChangesAsync();

            return results;

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

        //log clock in

        //log clock out

        public async Task<bool> IsClockedIn(int userId)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            return await dbContext.UserShifts
                .Where(x => x.UserID == userId)
                .Where(x => x.UserShiftStart < DateTime.Now)
                .Where(x => x.UserShiftEnd == null)
                .AnyAsync();
        }


        public async Task<Users?> GetLoggedInUser(int userId)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            return await dbContext.Users
                .Where(x => x.UserID == userId)
                .FirstOrDefaultAsync();
        }


        public async Task<int> AddUserStartTime(int userId)
        {
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            var newRecord = new UserShifts
            {
                UserID = userId,
                UserShiftStart = DateTime.Now
            };

            await dbContext.UserShifts.AddAsync(newRecord);

            await dbContext.CustomSaveChangesAsync();

            return  newRecord.UserShiftID;
        }

        public async Task<bool> AddUserEndTime(int userId)
        {
            //need a Primary Key to look it up.
            
            await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
            var recordToUpdate = await dbContext.UserShifts
                .Where(x => x.UserID == userId)
                .FirstOrDefaultAsync();

            recordToUpdate.UserShiftEnd = DateTime.Now;

            return await dbContext.CustomSaveChangesAsync() > 0; 

        }



    }
}
