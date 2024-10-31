using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs;

namespace FrontEnd.Pages.FootballPlayerPages
{
    public class DetailsModel : PageModel
    {
        private readonly BOs.EnglishPremierLeague2024DbContext _context;

        public DetailsModel(BOs.EnglishPremierLeague2024DbContext context)
        {
            _context = context;
        }

        public FootballPlayer FootballPlayer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footballplayer = await _context.FootballPlayers.FirstOrDefaultAsync(m => m.FootballPlayerId == id);
            if (footballplayer == null)
            {
                return NotFound();
            }
            else
            {
                FootballPlayer = footballplayer;
            }
            return Page();
        }
    }
}
