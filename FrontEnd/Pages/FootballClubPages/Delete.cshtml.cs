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
    public class DeleteModel : PageModel
    {
        private readonly BOs.EnglishPremierLeague2024DbContext _context;

        public DeleteModel(BOs.EnglishPremierLeague2024DbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FootballClub FootballClub { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footballclub = await _context.FootballClubs.FirstOrDefaultAsync(m => m.FootballClubId == id);

            if (footballclub == null)
            {
                return NotFound();
            }
            else
            {
                FootballClub = footballclub;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footballclub = await _context.FootballClubs.FindAsync(id);
            if (footballclub != null)
            {
                FootballClub = footballclub;
                _context.FootballClubs.Remove(FootballClub);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
