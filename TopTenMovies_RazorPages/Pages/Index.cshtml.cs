using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopTenMovies_RazorPages.Models.Data;
using TopTenMovies_RazorPages.Models;
using Microsoft.EntityFrameworkCore;

namespace TopTenMovies_RazorPages.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationContext _context;

        public IndexModel(ApplicationContext context)
        {
            _context = context;
        }

        public IList<Movie> Movies { get; set; }

        public bool ShowAddButton { get; set; } = true;

        public void OnGet()
        {
            Movies = _context.Movies.ToList();
        }
    }
}
