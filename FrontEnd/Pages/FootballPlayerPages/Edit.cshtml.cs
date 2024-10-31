using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Response;
using System.Net.Http;

namespace FrontEnd.Pages.FootballPlayerPages
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public FootballPlayer FootballPlayer { get; set; } = default!;

        [BindProperty]
        public IEnumerable<FootballClubResponse> FootballClubs { get; set; } = new List<FootballClubResponse>();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                // Call API to get football player by ID
                FootballPlayer = await _httpClient.GetFromJsonAsync<FootballPlayer>($"http://localhost:5009/api/FootballPlayer/get/{id}");

                if (FootballPlayer == null)
                {
                    return NotFound();
                }

                // Call API to retrieve all football clubs
                var response = await _httpClient.GetAsync("http://localhost:5009/api/FootballClub/getfootballclubs");

                if (response.IsSuccessStatusCode)
                {
                    FootballClubs = await response.Content.ReadFromJsonAsync<IEnumerable<FootballClubResponse>>() ?? new List<FootballClubResponse>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không thể tải danh sách câu lạc bộ.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Đã xảy ra lỗi: {ex.Message}");
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Call API to update the football player
                var response = await _httpClient.PutAsJsonAsync("http://localhost:5009/api/FootballPlayer/update", FootballPlayer);

                // Check if the response was successful
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index"); // Redirect to the Index page upon success
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không thể cập nhật cầu thủ bóng đá."); // Error message for failure
                    return Page(); // Return the page with the error message
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Đã xảy ra lỗi: {ex.Message}"); // Catch and display any errors
                return Page(); // Return the page with the error message
            }
        }


    }
}
