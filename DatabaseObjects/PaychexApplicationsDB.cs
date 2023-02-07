using Microsoft.EntityFrameworkCore;

namespace Paychex_SimpleTimeClock.DatabaseObjects
{
    public class PaychexApplicationsDB : DbContext
    {
        public PaychexApplicationsDB(DbContextOptions<PaychexApplicationsDB> options) : base(options)
        {
            
        }
        
       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       {
       } 

        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<AvailableShifts> AvailableShifts { get; set; }
        public virtual DbSet<AvailableBreaks> AvailableBreaks { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserShifts> UserShifts { get; set; }
        public virtual DbSet<UserBreaks> UserBreaks { get; set; }


    }
}
