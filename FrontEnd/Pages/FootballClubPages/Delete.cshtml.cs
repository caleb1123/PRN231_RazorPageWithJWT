using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs;
using BOs.Response;

namespace FrontEnd.Pages.FootballClubPages
{
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DeleteModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public FootballClubResponse FootballClub { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                // Call API to get football player by ID
                FootballClub = await _httpClient.GetFromJsonAsync<FootballClubResponse>($"http://localhost:5009/api/FootballClub/getfootballclubbyid/{id}");

                if (FootballClub == null)
                {
                    return NotFound();
                }
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Đã xảy ra lỗi: {ex.Message}");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(); // Ensure that the ID is not null or empty
            }

            try
            {
                // Call API to remove the football player
                var response = await _httpClient.DeleteAsync($"http://localhost:5009/api/FootballClub/deletefootballclub/{id}");

                // Check if the response was successful
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index"); // Redirect to the Index page upon success
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không thể xóa cầu thủ bóng đá."); // Error message for failure
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
