using Microsoft.AspNetCore.Mvc;

namespace MovieApp.Api.Controllers
{
    public class ActorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
