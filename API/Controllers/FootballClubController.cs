using BOs;
using BOs.Response;
using BOs.Resquest;
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

        [HttpPost("addfootballclub")] // Define a route for the add football club action
        public async Task<ActionResult<BOs.FootballClub>> AddFootballClub([FromBody] FootballClubRequest footballClub)
        {
            // Call the service to add a new football club
            BOs.FootballClub newFootballClub = await _footballClubService.AddFootballClub(footballClub);

            // Return the new football club details as JSON
            return Ok(newFootballClub); // HTTP 200 response with the new football club object
        }

        [HttpPut("updatefootballclub")] // Define a route for the update football club action
        public async Task<ActionResult<BOs.FootballClub>> UpdateFootballClub([FromBody] FootballClubRequest footballClub)
        {
            // Call the service to update an existing football club
            BOs.FootballClub updatedFootballClub = await _footballClubService.UpdateFootballClub(footballClub);

            // Return the updated football club details as JSON
            return Ok(updatedFootballClub); // HTTP 200 response with the updated football club object
        }

        [HttpDelete("deletefootballclub/{footballClubId}")] // Define a route for the delete football club action
        public async Task<ActionResult<bool>> DeleteFootballClub(string footballClubId)
        {
            // Call the service to delete a football club by id
            bool isDeleted = await _footballClubService.DeleteFootballClub(footballClubId);

            // Return a boolean value indicating if the football club was deleted
            return Ok(isDeleted); // HTTP 200 response with the deletion status
        }

    }
}
