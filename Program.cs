using Microsoft.EntityFrameworkCore;
using Paychex_SimpleTimeClock.DatabaseObjects;
using Paychex_SimpleTimeClock.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.AllowSynchronousIO = false;
    options.Limits.MaxRequestBodySize = 500 * 1024;
});

var app = await new ServiceBuilder().AddServices(builder);
var webApp = AppBuilder.ApplyConfiguration(app);

#region load data into memory

//Find the service layer within our scope.
using var scope = webApp.Services.CreateScope();
//Get the instance of BoardGamesDBContext in our services layer
var services = scope.ServiceProvider;
services.GetRequiredService<PaychexApplicationsDB>();

var _dataSeeder = services.GetService<IDataSeeder>();
await _dataSeeder.LoadTablesIntoMemory(services);

#endregion


await webApp.RunAsync();