using BOs.Resquest;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Impl;

namespace API.Controllers
{
    [ApiController] // Ensure that the controller is recognized as an API controller
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("loginbyjwt")] // Define a route for the login action
        public async Task<ActionResult<String>> LoginBỵwt([FromBody] LoginRequest request)
        {
            // Call the service to login
            String account = await _accountService.Login(request);


            // Return the account details as JSON if login is successful
            return Ok(account); // HTTP 200 response with the account object


        }
    }
}
