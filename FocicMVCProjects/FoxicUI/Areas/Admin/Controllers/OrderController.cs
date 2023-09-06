using Microsoft.AspNetCore.Mvc;

namespace FoxicUI.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
