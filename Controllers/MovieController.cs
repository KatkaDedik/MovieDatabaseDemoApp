using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Api.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
