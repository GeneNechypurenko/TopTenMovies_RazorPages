using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopTenMovies_RazorPages.Models.Data;
using TopTenMovies_RazorPages.Models;

namespace TopTenMovies_RazorPages.Pages
{
    public class AddModel : PageModel
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _environment;
        public bool ShowAddButton { get; set; }
        public bool ShowBackButton { get; set; }

        public AddModel(ApplicationContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Movie Movie { get; set; }
        [BindProperty]
        public IFormFile Poster { get; set; }

        public IActionResult OnGet()
        {
            ShowAddButton = false;
            ShowBackButton = true;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Poster != null && Poster.Length > 0)
            {
                string path = "/images/" + Poster.FileName;

                using (var fs = new FileStream(_environment.WebRootPath + path, FileMode.Create))
                {
                    await Poster.CopyToAsync(fs);
                }
                Movie.PosterUrl = path;
            }
            else
            {
                Movie.PosterUrl = "/background/default-poster.jpg";
            }

            _context.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
