using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StremingDemo.Models;

namespace StremingDemo.Controllers;

[Route("api/videos")]
[ApiController]
public class VideoController : ControllerBase
{
    private readonly string _videoPath = "wwwroot/videos/";

    [HttpGet("{fileName}")]
    public IActionResult GetVideo(string fileName)
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), _videoPath, fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound("Video not found");

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return File(stream, "video/mp4", enableRangeProcessing: true); // Enables Streming
    }

    [HttpGet("list")]
    public IActionResult GetVideos()
    {
        var files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), _videoPath), "*.mp4");

        List<Video> videos = new List<Video>();

        foreach (var file in files)
        {
            var video = new Video
            {
                Code = Path.GetFileName(file).Split('.')[0],
                Name = Path.GetFileName(file)
            };
            videos.Add(video);
        }

        videos = videos.OrderBy(v => v.Code).ToList();

        return Ok(videos);
    }

}

