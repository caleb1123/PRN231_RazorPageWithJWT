using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
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
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty(SupportsGet = true)]
        public IList<FootballPlayerDTO> FootballPlayer { get; set; } = default!;

        public string UserRole { get; private set; }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = string.Empty; // Bind SearchTerm for query parameters

        public async Task OnGetAsync()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            UserRole = !string.IsNullOrEmpty(token) ? GetUserRoleFromToken(token) : string.Empty;

            // Determine the appropriate API endpoint based on the search term
            if (string.IsNullOrEmpty(SearchTerm))
            {
                // Retrieve all players if no search term is provided
                FootballPlayer = await _httpClient.GetFromJsonAsync<List<FootballPlayerDTO>>("http://localhost:5009/api/FootballPlayer/getall");
            }
            else
            {
                // Retrieve players based on the search term
                FootballPlayer = await _httpClient.GetFromJsonAsync<List<FootballPlayerDTO>>($"http://localhost:5009/api/FootballPlayer/search/{SearchTerm}");
            }
        }

        private string GetUserRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                return string.Empty;
            }

            var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role");
            return roleClaim?.Value ?? string.Empty;
        }
    }
}
