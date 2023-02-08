using Microsoft.AspNetCore.Mvc;
using Paychex_SimpleTimeClock.DataAccess.Interface;
using Paychex_SimpleTimeClock.Models;
using System.Diagnostics;

namespace Paychex_SimpleTimeClock.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index() => View();
    }
}