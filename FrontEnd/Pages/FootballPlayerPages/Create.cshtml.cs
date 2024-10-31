using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs;

namespace FrontEnd.Pages.FootballPlayerPages
{
    public class CreateModel : PageModel
    {
        private readonly BOs.EnglishPremierLeague2024DbContext _context;

        public CreateModel(BOs.EnglishPremierLeague2024DbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["FootballClubId"] = new SelectList(_context.FootballClubs, "FootballClubId", "FootballClubId");
            return Page();
        }

        [BindProperty]
        public FootballPlayer FootballPlayer { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.FootballPlayers.Add(FootballPlayer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
