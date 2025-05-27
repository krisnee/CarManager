using CarManager.Core.Dtos;
using CarManager.Core.Models;
using CarManager.Core.ServiceInterfaces;
using CarManager.Data;
using Microsoft.EntityFrameworkCore;

namespace CarManager.ApplicationServices.Services
{
    public class CarService(CarDbContext context) : ICarService
    {
        private readonly CarDbContext _context = context;

        // Muutke DetailAsync → GetByIdAsync
        public async Task<Car> GetByIdAsync(Guid id)
        {
            var result = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            return result!;
        }

        // Muutke Create → CreateAsync
        public async Task<Car> CreateAsync(CarDto dto)
        {
            var car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = dto.Brand ?? "",
                Model = dto.Model ?? "",
                Year = dto.Year ?? 0,
                Color = dto.Color ?? "",
                Price = dto.Price ?? 0,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
            return car;
        }

        // Muutke Update → UpdateAsync
        public async Task<Car> UpdateAsync(CarDto dto)
        {
            var domain = new Car
            {
                Id = dto.Id ?? Guid.NewGuid(),
                Brand = dto.Brand ?? "",
                Model = dto.Model ?? "",
                Year = dto.Year ?? 0,
                Color = dto.Color ?? "",
                Price = dto.Price ?? 0,
                CreatedAt = dto.CreatedAt ?? DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
            _context.Cars.Update(domain);
            await _context.SaveChangesAsync();
            return domain;
        }

        // Muutke Delete → DeleteAsync
        public async Task<Car> DeleteAsync(Guid id)
        {
            var car = await _context.Cars
                .FirstOrDefaultAsync(x => x.Id == id);
            if (car == null)
                throw new Exception("Car not found");
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<IEnumerable<CarDto>> GetAllAsync()
        {
            return await _context.Cars
                .Select(car => new CarDto
                {
                    Id = car.Id,
                    Brand = car.Brand,
                    Model = car.Model,
                    Year = car.Year,
                    Color = car.Color,
                    Price = car.Price,
                    CreatedAt = car.CreatedAt,
                    ModifiedAt = car.ModifiedAt
                })
                .ToListAsync();
        }
    }
}