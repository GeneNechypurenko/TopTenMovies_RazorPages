using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopTenMovies_RazorPages.Models.Data;
using TopTenMovies_RazorPages.Models;

namespace TopTenMovies_RazorPages.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationContext _context;

        public DeleteModel(ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _context.Movies.FindAsync(id);

            if (Movie == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var movieToDelete = await _context.Movies.FindAsync(Movie.Id);

            if (movieToDelete == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movieToDelete);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
