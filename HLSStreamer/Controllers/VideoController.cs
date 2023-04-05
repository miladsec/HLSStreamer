using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace HLSStreamer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly ILogger<VideoController> _logger;
        private readonly IFileProvider _fileProvider;

        public VideoController(ILogger<VideoController> logger, IFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        public IActionResult CovertVideoFormat(string inputFilePath, string outPutFilePath)
        {
            var result = new FFmpeg(inputFilePath, outPutFilePath).ConvertMp4Tom3u8();

            return Content(result);
        }

        [HttpGet("{playListName}.m3u8")]
        public IActionResult GetVideoStream(string playListName)
        {
            var streamProvider = _fileProvider;
            var fileInfo = streamProvider.GetFileInfo($"{playListName}.m3u8");
            if (!fileInfo.Exists)
            {
                return NotFound();
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileInfo.Name, out var contentType))
            {
                contentType = "application/vnd.apple.mpegurl";
            }

            var stream = fileInfo.CreateReadStream();

            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return new FileStreamResult(stream, contentType);
        }

        [HttpGet("{fileName}.ts")]
        public IActionResult GetVideoChunk(string fileName)
        {
            var streamProvider = _fileProvider;
            var fileInfo = streamProvider.GetFileInfo($"{fileName}.ts");

            if (!fileInfo.Exists)
            {
                return NotFound();
            }

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileInfo.Name, out var contentType))
            {
                contentType = "video/mp2t";
            }

            var stream = fileInfo.CreateReadStream();

            Response.Headers.Add("Access-Control-Allow-Origin", "*");

            return new FileStreamResult(stream, contentType);
        }
    }
}
