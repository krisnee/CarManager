using CarManager.Core.Dtos;
using CarManager.Core.Models;
using CarManager.Core.ServiceInterfaces;
using CarManager.Data;
using Microsoft.EntityFrameworkCore;

namespace CarManager.ApplicationServices.Services
{
    public class CarService : ICarService
    {
        private readonly CarDbContext _context;

        public CarService(CarDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarDto>> GetAllAsync()
        {
            var cars = await _context.Cars.ToListAsync();
            return cars.Select(car => new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            });
        }

        public async Task<Car> GetByIdAsync(Guid id)
        {
            return await _context.Cars.FindAsync(id);
        }

        public async Task<Car> CreateAsync(CarDto dto)
        {
            var car = new Car
            {
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year ?? 0,
                Color = dto.Color,
                Price = dto.Price ?? 0,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        public async Task<Car> UpdateAsync(CarDto dto)
        {
            var car = await _context.Cars.FindAsync(dto.Id);
            if (car != null)
            {
                car.Brand = dto.Brand;
                car.Model = dto.Model;
                car.Year = dto.Year ?? car.Year;
                car.Color = dto.Color;
                car.Price = dto.Price ?? car.Price;
                car.ModifiedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
            return car;
        }

        public async Task<Car> DeleteAsync(Guid id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            return car;
        }
    }
}