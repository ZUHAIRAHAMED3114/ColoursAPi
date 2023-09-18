using ColoursAPi.Models;
using Microsoft.EntityFrameworkCore;
namespace ColoursAPi.Db
{
    public class ColorDbContext : DbContext
    {
        public DbSet<Color> Colors { get; set; }
        public ColorDbContext(DbContextOptions<ColorDbContext> options):base(options)
        {}

    }
}
