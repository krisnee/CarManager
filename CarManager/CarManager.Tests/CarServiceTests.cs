using CarManager.ApplicationServices.Services;
using CarManager.Core.Dtos;
using CarManager.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace CarManager.Tests
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
        public async Task Create_AddsCar()
        {
            var service = GetService();
            var carDto = GetTestCarDto();

            await service.Create(carDto);
            var cars = await service.GetAllAsync();

            Assert.Single(cars);
        }

        [Fact]
        public async Task DetailAsync_ReturnsCar()
        {
            var service = GetService();
            var carDto = GetTestCarDto();

            var createdCar = await service.Create(carDto);
            var fetchedCar = await service.DetailAsync(createdCar.Id);

            Assert.NotNull(fetchedCar);
            Assert.Equal("Test", fetchedCar.Brand);
        }

        [Fact]
        public async Task Update_UpdatesCar()
        {
            var service = GetService();
            var carDto = GetTestCarDto();

            var createdCar = await service.Create(carDto);
            var updatedDto = new CarDto
            {
                Id = createdCar.Id,
                Brand = "Test",
                Model = "Model",
                Year = 2020,
                Color = "Blue", // <-- muudame värvi
                Price = 10000,
                CreatedAt = createdCar.CreatedAt
            };

            await service.Update(updatedDto);
            var updatedCar = await service.DetailAsync(createdCar.Id);

            Assert.Equal("Blue", updatedCar.Color);
        }

        [Fact]
        public async Task Delete_RemovesCar()
        {
            var service = GetService();
            var carDto = GetTestCarDto();

            var createdCar = await service.Create(carDto);
            await service.Delete(createdCar.Id);

            var cars = await service.GetAllAsync();
            Assert.Empty(cars);
        }
    }
}
