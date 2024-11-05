using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.Response
{
    public class FootballClubResponse
    {
        [Key]
        public string FootballClubId { get; set; } = null!;

        public string ClubName { get; set; } = null!;

        public string ClubShortDescription { get; set; } = null!;

        public string SoccerPracticeField { get; set; } = null!;

        public string Mascos { get; set; } = null!;
    }
}
