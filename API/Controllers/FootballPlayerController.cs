using BOs;
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
        public async Task<IActionResult> UpdatePlayer([FromBody] FootballPlayer request)
        {
            if (request == null)
            {
                return BadRequest("Invalid player data.");
            }

            try
            {
                FootballPlayer response = await _service.UpdatePlayer(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("remove/{footballPlayerId}")]
        public async Task<IActionResult> RemovePlayer(string footballPlayerId)
        {
            if (string.IsNullOrEmpty(footballPlayerId))
            {
                return BadRequest("Invalid player id.");
            }

            try
            {
                bool response = await _service.RemovePlayer(footballPlayerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get/{footballPlayerId}")]
        public async Task<IActionResult> GetPlayer(string footballPlayerId)
        {
            if (string.IsNullOrEmpty(footballPlayerId))
            {
                return BadRequest("Invalid player id.");
            }

            try
            {
                FootballPlayer response = await _service.GetPlayer(footballPlayerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                List<FootballPlayerResponse> response = await _service.GetPlayers();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
