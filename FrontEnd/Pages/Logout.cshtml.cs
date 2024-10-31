using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages
{
    public class LogoutModel : PageModel
    {
        public IActionResult OnPost()
        {
            // Clear the session to log out the user
            HttpContext.Session.Clear();

            // Redirect to the Index page (login page)
            return RedirectToPage("/Index");
        }
    }
}
