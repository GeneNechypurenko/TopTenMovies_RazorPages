using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopTenMovies_RazorPages.Models.Data;
using TopTenMovies_RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace TopTenMovies_RazorPages.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationContext _context;

        public DetailsModel(ApplicationContext context)
        {
            _context = context;
        }

        public Movie Movie { get; set; }

        public bool ShowBackButton { get; set; } = true;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _context.Movies.FindAsync(id);

            if (Movie == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
