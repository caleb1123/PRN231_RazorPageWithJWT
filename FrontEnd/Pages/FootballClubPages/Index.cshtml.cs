using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs;

namespace FrontEnd.Pages.FootballClubPages
{
    public class IndexModel : PageModel
    {
        private readonly BOs.EnglishPremierLeague2024DbContext _context;

        public IndexModel(BOs.EnglishPremierLeague2024DbContext context)
        {
            _context = context;
        }

        public IList<FootballClub> FootballClub { get;set; } = default!;

        public async Task OnGetAsync()
        {
            FootballClub = await _context.FootballClubs.ToListAsync();
        }
    }
}
