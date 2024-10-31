using System.ComponentModel.DataAnnotations;

namespace FrontEnd.DTO
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; } // Thêm thuộc tính Message

    }
}
