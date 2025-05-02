using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GameLauncher.Server.Controllers
{
    [ApiController]
    [Route("launcher")]
    public class LauncherController : ControllerBase
    {
        private readonly string _root;

        public LauncherController(IWebHostEnvironment env)
        {
            _root = Path.Combine(env.ContentRootPath, "wwwroot");
        }

        [HttpGet("banners.json")]
        [Produces("application/json")]
        public IActionResult GetBanners()
        {
            var path = Path.Combine(_root, "game/banners.json");
            if (!System.IO.File.Exists(path))
                return NotFound("Banners list not found.");

            var json = System.IO.File.ReadAllText(path);
            return Content(json, "application/json");
        }

        [HttpGet("news.json")]
        [Produces("application/json")]
        public IActionResult GetNews()
        {
            var path = Path.Combine(_root, "game/news.json");
            if (!System.IO.File.Exists(path))
                return NotFound("News list not found.");

            var json = System.IO.File.ReadAllText(path);
            return Content(json, "application/json");
        }
    }
}