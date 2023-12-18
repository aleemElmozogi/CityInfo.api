using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {

        private readonly FileExtensionContentTypeProvider _extensionContentTypeProvider;
        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider) { 
        _extensionContentTypeProvider= fileExtensionContentTypeProvider?? throw new System.ArgumentException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{fileId}")]
        public ActionResult getFile( string fileId) {
            var pathToFile = "hr.masarat.overtime.pdf";
            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound();
            }
            if (!_extensionContentTypeProvider.TryGetContentType(
                pathToFile, out var fileType
                )) { 
            fileType= "application/octet-stream";
            }
            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, fileType, Path.GetFileName(pathToFile));
        }
    }
}
