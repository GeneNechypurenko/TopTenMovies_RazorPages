using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using TopTenMovies_RazorPages.Models;
using TopTenMovies_RazorPages.Models.Data;

namespace TopTenMovies_RazorPages.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(ApplicationContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Movie = await _context.Movies.FindAsync(id);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile poster)
        {
            var movieToUpdate = await _context.Movies.FindAsync(id);

            if (movieToUpdate == null)
            {
                return NotFound();
            }

            if (poster != null && poster.Length > 0)
            {
                if (!string.IsNullOrEmpty(movieToUpdate.PosterUrl))
                {
                    var oldPath = Path.Combine(_environment.WebRootPath, movieToUpdate.PosterUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                var fileName = Path.GetFileName(poster.FileName);
                var path = Path.Combine(_environment.WebRootPath, "images", fileName);

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    await poster.CopyToAsync(fs);
                }

                movieToUpdate.PosterUrl = $"/images/{fileName}";
            }
            else
            {
                movieToUpdate.PosterUrl = movieToUpdate.PosterUrl;
            }

            movieToUpdate.Title = Movie.Title;
            movieToUpdate.Rating = Movie.Rating;
            movieToUpdate.Genre = Movie.Genre;
            movieToUpdate.Year = Movie.Year;
            movieToUpdate.Director = Movie.Director;
            movieToUpdate.Description = Movie.Description;

            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}
