using Paychex_SimpleTimeClock.DataAccess.Repository;

namespace Paychex_SimpleTimeClock.Startup
{
    public class AppBuilder
    {
        public static WebApplication ApplyConfiguration(WebApplication webApplication)
        {
            // Configure the HTTP request pipeline.            
            if (!webApplication.Environment.IsDevelopment())
            {
                webApplication.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.                
                webApplication.UseHsts();
            }


            webApplication.UseHttpsRedirection();

            //Enable use of Session            
            webApplication.UseSession();
            webApplication.UseHttpsRedirection();
            webApplication.UseStaticFiles();
            webApplication.UseRouting();
            //allows for JavaScript            
            webApplication.UseResponseCompression();
            webApplication.UseWebOptimizer();

            if (webApplication.Environment.IsDevelopment())
            {
                //add for windows auth to server                
                webApplication.UseAuthentication();
                webApplication.UseAuthorization();
            }

            webApplication.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

            return webApplication;
            
        }
    }
}