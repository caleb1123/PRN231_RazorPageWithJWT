using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.DTO; // Đảm bảo rằng namespace này chứa LoginModel
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HttpClient _httpClient;

        [BindProperty]
        public LoginModel Login { get; set; } = new LoginModel(); // Khởi tạo LoginModel

        public IndexModel(ILogger<IndexModel> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public void OnGet()
        {
            // Xử lý GET nếu cần
        }

        public async Task<IActionResult> OnPost()
        {
            // Tạo request body
            var requestBody = new
            {
                email = Login.Email,
                password = Login.Password
            };

            // Gọi API
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5009/api/Account/loginbyjwt", requestBody);

            if (response.IsSuccessStatusCode)
            {
                // Nếu thành công, chuyển hướng đến trang mong muốn
                return RedirectToPage("/FootballPlayerPages/Index"); // Thay đổi theo trang đích của bạn
            }

            // Nếu sai, gán thông báo lỗi
            Login.Message = "Invalid email or password.";
            return Page(); // Trả về trang với thông báo lỗi
        }
    }
}
