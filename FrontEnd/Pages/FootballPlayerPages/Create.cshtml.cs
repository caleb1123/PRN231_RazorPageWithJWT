using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs;
using BOs.Response;

namespace FrontEnd.Pages.FootballPlayerPages
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public FootballPlayer FootballPlayer { get; set; } = default!;

        [BindProperty]
        public IEnumerable<FootballClubResponse> FootballClubs { get; set; } = new List<FootballClubResponse>();

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Gọi API để thêm cầu thủ bóng đá mới
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5009/api/FootballPlayer/add", FootballPlayer);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không thể thêm cầu thủ bóng đá.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Đã xảy ra lỗi: {ex.Message}");
                return Page();
            }
        }
    }
}
