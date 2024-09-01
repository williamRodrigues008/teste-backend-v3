using Microsoft.AspNetCore.Mvc;

namespace TheatricalPlayersRefactoringKata.Server.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("get")]
        public IActionResult Get()
        {
            return View();
        }

    }
}
