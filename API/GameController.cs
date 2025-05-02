using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace GameLauncher.Server.Controllers
{
    [ApiController]
    [Route("game")]
    public class GameController : ControllerBase
    {
        private readonly string _root;

        public GameController(IWebHostEnvironment env)
        {
            _root = Path.Combine(env.ContentRootPath, "wwwroot");
        }

        [HttpGet("version.txt")]
        [Produces("text/plain")]
        public IActionResult GetVersion()
        {
            var path = Path.Combine(_root, "game/version.txt");
            if (!System.IO.File.Exists(path))
                return NotFound("Version file not found.");

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "text/plain");
        }

        [HttpGet("manifest.json")]
        [Produces("application/json")]
        public IActionResult GetManifest()
        {
            var path = Path.Combine(_root, "game/manifest.json");
            if (!System.IO.File.Exists(path))
                return NotFound("Manifest not found.");

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "application/json");
        }

        [HttpGet("files/{*filepath}")]
        public IActionResult GetFile(string filepath)
        {
            var path = Path.Combine(_root, "game/files", filepath);
            if (!System.IO.File.Exists(path))
                return NotFound();

            var contentType = filepath.EndsWith(".json") ? "application/json" : "application/octet-stream";
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, contentType);
        }
    }
}
