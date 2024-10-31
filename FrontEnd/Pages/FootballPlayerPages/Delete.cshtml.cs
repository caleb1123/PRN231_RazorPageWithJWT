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
    public class DeleteModel : PageModel
    {
        private readonly BOs.EnglishPremierLeague2024DbContext _context;

        public DeleteModel(BOs.EnglishPremierLeague2024DbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footballplayer = await _context.FootballPlayers.FindAsync(id);
            if (footballplayer != null)
            {
                FootballPlayer = footballplayer;
                _context.FootballPlayers.Remove(FootballPlayer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
