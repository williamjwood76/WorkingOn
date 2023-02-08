using Microsoft.AspNetCore.Mvc;
using Paychex_SimpleTimeClock.DataAccess.Interface;

namespace Paychex_SimpleTimeClock.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IPaychexDataAccess _paychexDataAccess;
        public RegisterController(IPaychexDataAccess paychexDataAccess)
        {
            _paychexDataAccess = paychexDataAccess;
        }
        // GET: TimeClockController
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> RegisterEmployee([FromBody] List<KeyValuePair<string, object>> obj)
        {
            var userName = obj.GetStringValue("Username");
            var password = obj.GetStringValue("Password");
            return Json(await _paychexDataAccess.RegisterEmployee(userName, password));
        }
    }
}
