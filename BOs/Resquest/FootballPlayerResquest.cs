using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.Resquest
{
    public class FootballPlayerResquest
    {
        [Required(ErrorMessage = "Football Player ID is required.")]
        public string FootballPlayerId { get; set; } = null!;

        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^([A-Z][a-zA-Z0-9@#]*\s)*[A-Z][a-zA-Z0-9@#]*$", ErrorMessage = "Each word of Full Name must begin with a capital letter and can include a-z, A-Z, 0-9, spaces, @, #.")]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Achievements is required.")]
        [StringLength(100, MinimumLength = 9, ErrorMessage = "Achievements must be between 9 and 100 characters.")]
        public string Achievements { get; set; } = null!;

        [Required(ErrorMessage = "Birthday is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(FootballPlayerResquest), nameof(ValidateBirthday))]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Player Experiences is required.")]
        public string PlayerExperiences { get; set; } = null!;

        [Required(ErrorMessage = "Nomination is required.")]
        [StringLength(100, MinimumLength = 9, ErrorMessage = "Nomination must be between 9 and 100 characters.")]
        public string Nomination { get; set; } = null!;

        public string? FootballClubId { get; set; }

        public static ValidationResult? ValidateBirthday(DateTime? birthday, ValidationContext context)
        {
            if (birthday == null)
            {
                return new ValidationResult("Birthday is required.");
            }

            var minDate = new DateTime(2007, 1, 1);
            if (birthday >= minDate)
            {
                return new ValidationResult("Birthday must be before 01-01-2007.");
            }
            return ValidationResult.Success;
        }

    }
}
