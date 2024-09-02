using Microsoft.AspNetCore.Mvc;
using TheatricalPlayersRefactoringKata.Server.ContextDb;
using TheatricalPlayersRefactoringKata.Server.Entities;
using TheatricalPlayersRefactoringKata.Server.Interfaces;

namespace TheatricalPlayersRefactoringKata.Server.Controllers
{
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ContextDtaBase _context;
        private readonly PlayInterface _playInterface;

        public HomeController(ContextDtaBase context, PlayInterface playInterface)
        {
            _context = context;
            _playInterface = playInterface;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            return Json( await _playInterface.GetAllPlay());
            
        }

        [HttpPost("CreateNewPlay")]
        public async Task<IActionResult> CreateNewPlay([FromBody]Play play)
        {
            _playInterface.NewPlay(play);
            if (await _playInterface.SavePlay())
            {
                return Json("Congrats! your play was created with successfully");
            }
            else
            {
                return Json(BadRequest("OPS! your play presented a problem when creating"));
            }
        }

        [HttpDelete("DeletePlay")]
        public async Task<IActionResult> DeletePlay([FromBody]Play playId)
        {
            _playInterface.DeletePlay(playId);
            if (await _playInterface.SavePlay())
            {
                return Json("Successfully !");
            }
            else
            {
                return Json(BadRequest("Falha ao excluir o produto"));
            }
        }

        [HttpPut("UpdatePlay")]
        public async Task<IActionResult> UpdatePlay([FromBody]Play play)
        {
            _playInterface.UpdatePlay(play);
            if (await _playInterface.SavePlay())
            {
                return Json("Play updated with successfully!");
            }
            else
            {
                return Json(BadRequest("Update play failed"));
            }
        }

    }
}
