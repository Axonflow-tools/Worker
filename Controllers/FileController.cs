using Microsoft.AspNetCore.Mvc;
using Worker.Services;

namespace Worker.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    [HttpPost("set-path")]
    public IActionResult SetCurrentFile([FromBody] FilePathModel request)
    {
        if (string.IsNullOrWhiteSpace(request.Path))
        {
            return BadRequest("File path required");
        }

        FileService.CurrentFile = request.Path;
        return Ok(new { Message = $"File path set to {request.Path}" });
    }
}

public class FilePathModel
{
    public required string Path { get; set; }
}