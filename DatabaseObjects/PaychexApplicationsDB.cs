﻿using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Paychex_SimpleTimeClock.DatabaseObjects
{
    public class PaychexApplicationsDB : DbContext
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        
        public PaychexApplicationsDB(DbContextOptions<PaychexApplicationsDB> options, IWebHostEnvironment webHostEnvironment) : base(options)
        {
            _webHostEnvironment = webHostEnvironment;
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



        public async Task<int> CustomSaveChangesAsync()
        {
            var context = _webHostEnvironment.ContentRootPath;

            //Todo:  for all files
            await File.WriteAllTextAsync($"{context}Resources\\Roles.json", JsonSerializer.Serialize(await Roles.ToListAsync()));
            await File.WriteAllTextAsync($"{context}Resources\\UserShifts.json", JsonSerializer.Serialize(await UserShifts.ToListAsync()));


            return await SaveChangesAsync();
        }

    }
}
