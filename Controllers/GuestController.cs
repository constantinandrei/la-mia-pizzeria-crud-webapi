using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class GuestController : Controller
    {
        public IActionResult Index()
        {
            ViewData["title"] = "Pizza";
            return View();
        }

        public IActionResult Details(int id)
        {
            return View(id);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
