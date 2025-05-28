using CarManager.ApplicationServices.Services;
using CarManager.Core.Dtos;
using CarManager.Core.ServiceInterfaces;
using CarManager.Data;
using Microsoft.EntityFrameworkCore;

namespace CarManager.Tests
{
    public class CarServiceTests
    {
        private CarService GetService()
        {
            var options = new DbContextOptionsBuilder<CarDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var context = new CarDbContext(options);
            return new CarService(context);
        }

        private CarDto GetTestCarDto()
        {
            return new CarDto
            {
                Brand = "Test",
                Model = "Model",
                Year = 2020,
                Color = "Red",
                Price = 10000
            };
        }

        [Fact]
        public async Task CreateAsync_AddsCar()
        {
            var service = GetService();
            var carDto = GetTestCarDto();
            await service.CreateAsync(carDto);
            var cars = await service.GetAllAsync();
            Assert.Single(cars);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCar()
        {
            var service = GetService();
            var carDto = GetTestCarDto();
            var createdCar = await service.CreateAsync(carDto);
            var fetchedCar = await service.GetByIdAsync(createdCar.Id);
            Assert.NotNull(fetchedCar);
            Assert.Equal("Test", fetchedCar.Brand);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesCar()
        {
            var service = GetService();
            var carDto = GetTestCarDto();
            var createdCar = await service.CreateAsync(carDto);

            var updatedDto = new CarDto
            {
                Id = createdCar.Id,
                Brand = "UpdatedBrand",
                Model = "UpdatedModel",
                Year = 2021,
                Color = "Blue",
                Price = 15000,
                CreatedAt = createdCar.CreatedAt,
                ModifiedAt = DateTime.UtcNow
            };

            await service.UpdateAsync(updatedDto);
            var updatedCar = await service.GetByIdAsync(createdCar.Id);

            Assert.Equal("Blue", updatedCar.Color);
            Assert.Equal("UpdatedBrand", updatedCar.Brand);
            Assert.Equal(15000, updatedCar.Price);
        }

        [Fact]
        public async Task DeleteAsync_RemovesCar()
        {
            var service = GetService();
            var carDto = GetTestCarDto();
            var createdCar = await service.CreateAsync(carDto);
            await service.DeleteAsync(createdCar.Id);
            var cars = await service.GetAllAsync();
            Assert.Empty(cars);
        }
    }
}