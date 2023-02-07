using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paychex_SimpleTimeClock.DataAccess.Interface;
using Paychex_SimpleTimeClock.DataAccess.Repository;
using Paychex_SimpleTimeClock.Models;
using System.Diagnostics;

namespace Paychex_SimpleTimeClock.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPaychexDataAccess _paychexDataAccess;

        public HomeController(IPaychexDataAccess payChexDataAccess) 
        {
            _paychexDataAccess = payChexDataAccess;
        }


        //Get 
        //public IActionResult Index([FromQuery] variableName)
        //Post
        //public IActionResult Index([FromBody] variableName)

        public IActionResult Index() => View();

        public async Task<IActionResult> AuthenticatePaychex([FromBody] List<KeyValuePair<string, object>> obj)
        {
            var userId = (await _paychexDataAccess.Login(obj.GetStringValue("Username"), obj.GetStringValue("Password")));
            HttpContext.Session.SetString("UserID", userId);
            return !string.IsNullOrWhiteSpace(userId)
                ? View("~/Views/TimeClock/Index.cshtml")
                : Json(false);
        }
            
        



        public IActionResult Privacy() => View();

        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}