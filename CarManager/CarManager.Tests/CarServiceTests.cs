using Xunit;
using Car.Application.Services;
using Car.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Car.Core.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace Car.Tests
{
    public class CarServiceTests
    {
        private CarService GetService()
        {
            var options = new DbContextOptionsBuilder<CarDbContext>()
                .UseInMemoryDatabase(databaseName: "CarTestDb")
                .Options;
            var context = new CarDbContext(options);
            return new CarService(context);
        }

        [Fact]
        public async Task CreateAsync_AddsCar()
        {
            var service = GetService();
            var car = new CarEntity { Make = "Test", Model = "Model", Year = 2020, Color = "Red", Price = 10000 };
            await service.CreateAsync(car);
            var cars = await service.GetAllAsync();
            Assert.Single(cars);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCar()
        {
            var service = GetService();
            var car = new CarEntity { Make = "Test", Model = "Model", Year = 2020, Color = "Red", Price = 10000 };
            await service.CreateAsync(car);
            var fetchedCar = await service.GetByIdAsync(car.Id);
            Assert.NotNull(fetchedCar);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesCar()
        {
            var service = GetService();
            var car = new CarEntity { Make = "Test", Model = "Model", Year = 2020, Color = "Red", Price = 10000 };
            await service.CreateAsync(car);
            car.Color = "Blue";
            await service.UpdateAsync(car);
            var updatedCar = await service.GetByIdAsync(car.Id);
            Assert.Equal("Blue", updatedCar.Color);
        }

        [Fact]
        public async Task DeleteAsync_RemovesCar()
        {
            var service = GetService();
            var car = new CarEntity { Make = "Test", Model = "Model", Year = 2020, Color = "Red", Price = 10000 };
            await service.CreateAsync(car);
            await service.DeleteAsync(car.Id);
            var cars = await service.GetAllAsync();
            Assert.Empty(cars);
        }
    }
}