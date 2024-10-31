using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs;

namespace FrontEnd.Pages.FootballClubPages
{
    public class EditModel : PageModel
    {
        private readonly BOs.EnglishPremierLeague2024DbContext _context;

        public EditModel(BOs.EnglishPremierLeague2024DbContext context)
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

            var footballclub =  await _context.FootballClubs.FirstOrDefaultAsync(m => m.FootballClubId == id);
            if (footballclub == null)
            {
                return NotFound();
            }
            FootballClub = footballclub;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(FootballClub).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FootballClubExists(FootballClub.FootballClubId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FootballClubExists(string id)
        {
            return _context.FootballClubs.Any(e => e.FootballClubId == id);
        }
    }
}
