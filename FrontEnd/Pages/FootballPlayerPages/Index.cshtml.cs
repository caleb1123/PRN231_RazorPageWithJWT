using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json; // Make sure to include this for JSON deserialization
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BOs;
using BOs.Response;
using FrontEnd.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace FrontEnd.Pages.FootballPlayerPages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient; // Declare HttpClient instance

        public IndexModel(HttpClient httpClient) // Inject HttpClient
        {
            _httpClient = httpClient;
        }

        public IList<FootballPlayerDTO> FootballPlayer { get; set; } = default!; // Use FootballPlayerResponse

        public string UserRole { get; private set; }

        public async Task OnGetAsync()
        {
            try
            {
                var token = HttpContext.Session.GetString("AuthToken");

                if (!string.IsNullOrEmpty(token))
                {
                    UserRole = GetUserRoleFromToken(token);
                }
                else
                {
                    UserRole = string.Empty; // No role if token is missing
                }

                // Call the API to get all football players
                FootballPlayer = await _httpClient.GetFromJsonAsync<List<FootballPlayerDTO>>("http://localhost:5009/api/FootballPlayer/getall");
            }
            catch (HttpRequestException e)
            {
                // Handle error (log it, show message, etc.)
                Console.WriteLine($"Request error: {e.Message}");
                FootballPlayer = new List<FootballPlayerDTO>(); // Set to empty list on error
            }
        }

        private string GetUserRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            // Check if jwtToken is valid
            if (jwtToken == null)
            {
                return string.Empty;
            }

            // Retrieve the role claim
            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role");

            return roleClaim?.Value ?? string.Empty;
        }
    }
}
