using System.Data;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Paychex_SimpleTimeClock.DatabaseObjects;

namespace Paychex_SimpleTimeClock.Startup
{
    public class DataSeeder : IDataSeeder
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DataSeeder(IWebHostEnvironment webHostEnvironment) 
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task LoadTablesIntoMemory(IServiceProvider serviceProvider)
        {
            await using var dbContext = new PaychexApplicationsDB(serviceProvider.GetRequiredService<DbContextOptions<PaychexApplicationsDB>>(), _webHostEnvironment);
            var context = _webHostEnvironment.ContentRootPath;

            if (!dbContext.Roles.Any())
                await dbContext.AddRangeAsync(  await LoadRolesTable(context));

            if (!dbContext.AvailableShifts.Any())
                await dbContext.AddRangeAsync(await LoadAvailableShifts(context));

            if (!dbContext.AvailableBreaks.Any())
                await dbContext.AddRangeAsync(await LoadAvailableBreaks(context));

            if (!dbContext.Users.Any())
                await dbContext.AddRangeAsync(await LoadUsersTable(context));

            if (!dbContext.UserShifts.Any())
                await dbContext.AddRangeAsync(await LoadUserShiftsTable(context));

            if (!dbContext.UserBreaks.Any())
                await dbContext.AddRangeAsync(await LoadUserBreaksTable(context));

            await dbContext.SaveChangesAsync();
        }

        private static async Task<IEnumerable<Roles>> LoadRolesTable(string context)
        {
            var fileContents = await File.ReadAllBytesAsync($"{context}Resources\\Roles.json");
            return JsonSerializer.Deserialize<IEnumerable<Roles>>(fileContents) ?? throw new FileNotFoundException("Unable to find file at specified location.");
        }
        private static async Task<IEnumerable<AvailableShifts>> LoadAvailableShifts(string context)
        {
            var fileContents = await File.ReadAllBytesAsync($"{context}Resources\\AvailableShifts.json");
            return JsonSerializer.Deserialize<IEnumerable<AvailableShifts>>(fileContents) ?? throw new FileNotFoundException("Unable to find file at specified location.");
        }
        private static async Task<IEnumerable<AvailableBreaks>> LoadAvailableBreaks(string context)
        {
            var fileContents = await File.ReadAllBytesAsync($"{context}Resources\\AvailableBreaks.json");
            return JsonSerializer.Deserialize<IEnumerable<AvailableBreaks>>(fileContents) ?? throw new FileNotFoundException("Unable to find file at specified location.");
        }
        private static async Task<IEnumerable<Users>> LoadUsersTable(string context)
        {
            var fileContents = await File.ReadAllBytesAsync($"{context}Resources\\Users.json");
            return JsonSerializer.Deserialize<IEnumerable<Users>>(fileContents) ?? throw new FileNotFoundException("Unable to find file at specified location.");
        }
        private static async Task<IEnumerable<UserShifts>> LoadUserShiftsTable(string context)
        {
            var fileContents = await File.ReadAllBytesAsync("Resources/UserShifts.json");
            return fileContents.Length == 0
                ? new List<UserShifts>()
                : JsonSerializer.Deserialize<IEnumerable<UserShifts>>(fileContents) ?? throw new FileNotFoundException("Unable to find file at specified location.");
        }
        private static async Task<IEnumerable<UserBreaks>> LoadUserBreaksTable(string context)
        {
            var fileContents = await File.ReadAllBytesAsync("Resources/UserBreaks.json");
            return fileContents.Length == 0
                ? new List<UserBreaks>() 
                : JsonSerializer.Deserialize<IEnumerable<UserBreaks>>(fileContents) ?? throw new FileNotFoundException("Unable to find file at specified location.");
        }

    }
}
