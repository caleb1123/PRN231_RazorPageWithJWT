using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs;
using FrontEnd.DTO;
using BOs.Response;

namespace FrontEnd.Pages.FootballClubPages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient; // Declare HttpClient instance

        public IndexModel(HttpClient httpClient) // Inject HttpClient
        {
            _httpClient = httpClient;
        }

        public IList<FootballClubResponse> FootballClub { get;set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {


                // Call the API to get all football players
                FootballClub = await _httpClient.GetFromJsonAsync<List<FootballClubResponse>>("http://localhost:5009/api/FootballClub/getfootballclubs");
            }
            catch (HttpRequestException e)
            {
                // Handle error (log it, show message, etc.)
                Console.WriteLine($"Request error: {e.Message}");
                FootballClub = new List<FootballClubResponse>(); // Set to empty list on error
            }
        }
    }
}
