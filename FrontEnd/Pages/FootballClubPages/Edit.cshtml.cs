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

namespace FrontEnd.Pages.FootballClubPages
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public BOs.FootballClub FootballClub { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            try
            {
                // Call API to get football player by ID
                FootballClub = await _httpClient.GetFromJsonAsync<BOs.FootballClub>($"http://localhost:5009/api/FootballClub/getfootballclubbyid/{id}");

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
                var response = await _httpClient.PutAsJsonAsync("http://localhost:5009/api/FootballClub/updatefootballclub", FootballClub);

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
