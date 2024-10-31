using BOs.Response;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers
{
    [ApiController] // Ensure that the controller is recognized as an API controller
    [Route("api/[controller]")]
    public class FootballClubController : ControllerBase
    {
        private readonly IFootballClubService _footballClubService;
        public FootballClubController(IFootballClubService footballClubService)
        {
            _footballClubService = footballClubService;
        }

        [HttpGet("getfootballclubbyid/{footballClubId}")] // Define a route for the get football club by id action
        public async Task<ActionResult<FootballClubResponse>> GetFootballClubById(string footballClubId)
        {
            // Call the service to get the football club by id
            FootballClubResponse footballClub = await _footballClubService.GetFootballClubById(footballClubId);

            // Return the football club details as JSON if found
            return Ok(footballClub); // HTTP 200 response with the football club object
        }

        [HttpGet("getfootballclubs")] // Define a route for the get football clubs action
        public async Task<ActionResult<List<FootballClubResponse>>> GetFootballClubs()
        {
            // Call the service to get all football clubs
            List<FootballClubResponse> footballClubs = await _footballClubService.GetFootballClubs();

            // Return the football clubs as JSON
            return Ok(footballClubs); // HTTP 200 response with the football clubs list
        }


    }
}
