using BOs.Response;
using BOs.Resquest;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FootballPlayerController : ControllerBase
    {
        private readonly IFootballPlayerService _service;

        public FootballPlayerController(IFootballPlayerService service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPlayer([FromBody] FootballPlayerResquest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid player data.");
            }

            try
            {
                FootballPlayerResponse response = await _service.AddPlayer(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePlayer([FromBody] FootballPlayerResquest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid player data.");
            }

            try
            {
                FootballPlayerResponse response = await _service.UpdatePlayer(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
