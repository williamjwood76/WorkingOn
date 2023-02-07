using Microsoft.AspNetCore.Mvc;
using Paychex_SimpleTimeClock.Authorization;
using Paychex_SimpleTimeClock.Authorization.Repository;
using Paychex_SimpleTimeClock.DataAccess.Interface;

namespace Paychex_SimpleTimeClock.Controllers
{
    [RequiresPermission(Permission.Employee, Permission.Admin)]
    public class TimeClockController : Controller
    {
        private readonly IPaychexDataAccess _paychexDataAccess;
        public TimeClockController(IPaychexDataAccess paychexDataAccess)
        {
            _paychexDataAccess  = paychexDataAccess;
        }



        // GET: TimeClockController
        public async Task<IActionResult> Index()
        {
            var someValue = await _paychexDataAccess.GetAvailableBreaks();
            return View(someValue.ToList());
        } 

        // GET: TimeClockController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}
        //
        //// GET: TimeClockController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}
        //
        //// POST: TimeClockController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //
        //// GET: TimeClockController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}
        //
        //// POST: TimeClockController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        //
        //// GET: TimeClockController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}
        //
        //// POST: TimeClockController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
