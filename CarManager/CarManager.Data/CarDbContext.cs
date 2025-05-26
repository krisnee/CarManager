using CarManager.Core.Domain.Models;
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
    }
}
