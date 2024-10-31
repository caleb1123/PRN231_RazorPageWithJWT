using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.DTO; // Đảm bảo rằng namespace này chứa LoginModel
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
                var token = await response.Content.ReadAsStringAsync(); // Đọc token từ response

                // Giải mã token để lấy role
                var role = GetUserRoleFromToken(token);

                if (token == null) // Kiểm tra xem role có phải là admin không
                {
                    Login.Message = "Bạn không có quyền truy cập vào trang này."; // Thông báo lỗi nếu không phải admin
                    return Page(); // Trả về trang với thông báo lỗi
                }
                else
                {
                    return RedirectToPage("/FootballPlayerPages/Index"); // Chuyển hướng nếu là admin
                    
                }
            }

            // Nếu sai, gán thông báo lỗi
            Login.Message = "Invalid email or password.";
            return Page(); // Trả về trang với thông báo lỗi
        }

        private string GetUserRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            try
            {
                // Giải mã token
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                // Lưu token vào Session
                HttpContext.Session.SetString("AuthToken", token);


                // Kiểm tra nếu jwtToken không hợp lệ
                if (jwtToken == null)
                {
                    return string.Empty; // Hoặc xử lý lỗi nếu cần
                }

                // Lấy thông tin claim từ token
                // Đảm bảo rằng claim type "role" là chính xác. Thay đổi tên nếu cần.
                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role"); // Thay "role" bằng ClaimTypes.Role nếu sử dụng

                // Nếu tìm thấy claim role, trả về giá trị của nó, ngược lại trả về string.Empty
                return roleClaim?.Value ?? string.Empty; // Trả về giá trị role, hoặc string.Empty nếu không tìm thấy
            }
            catch (Exception ex)
            {
                // Xử lý bất kỳ lỗi nào xảy ra trong quá trình đọc token
                Console.WriteLine($"Lỗi khi phân tích token: {ex.Message}");
                return string.Empty; // Hoặc xử lý lỗi khác nếu cần
            }
        }

    }
}
