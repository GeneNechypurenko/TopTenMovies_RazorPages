using Microsoft.EntityFrameworkCore;

namespace TopTenMovies_RazorPages.Models.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }
    }
}
