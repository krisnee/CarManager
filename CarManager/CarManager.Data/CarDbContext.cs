using CarManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Data
{
    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasPrecision(18, 2); // 18 numbrit kokku, 2 kohta peale koma
        }
    }
}
