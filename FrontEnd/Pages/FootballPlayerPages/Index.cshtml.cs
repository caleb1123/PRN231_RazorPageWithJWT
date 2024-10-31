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

        public async Task OnGetAsync()
        {
            try
            {
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
    }
}
