using ArthWeight.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
  public class AppController : Controller
  {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(WeightViewModel weightViewModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.UserMessage = "Weight added";
                ModelState.Clear();
            }
            return View();
        }

        public IActionResult History()
        {
            return View();
        }
    }
}
