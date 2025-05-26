using CarManager.Core.Domain.Models;
using CarManager.Core.Dto;
using CarManager.Core.Models;
using CarManager.Data;
using Microsoft.EntityFrameworkCore;

namespace ApplicationServices
{
    public class CarService
    {
        private readonly CarDbContext _context;

        public CarService(CarDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CarDto>> GetAllAsync()
        {
            var cars = await _context.Cars.ToListAsync();

            return cars.Select(c => new CarDto
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                Year = c.Year,
                Color = c.Color,
                Price = c.Price,
                CreatedAt = c.CreatedAt,
                ModifiedAt = c.ModifiedAt
            });
        }

        public async Task<CarDto?> GetByIdAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return null;

            return new CarDto
            {
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };
        }

        public async Task CreateAsync(CarDto carDto)
        {
            var car = new Car
            {
