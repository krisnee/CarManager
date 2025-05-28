using CarManager.Core.Dtos;
using CarManager.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarManager.Web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carService.GetAllAsync();
            return View(cars);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CarDto car)
        {
            if (!ModelState.IsValid)
            {
                return View(car);  // Näitab vigu
            }

            await _carService.CreateAsync(car);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null) return NotFound();

            var dto = new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };

            return View(dto);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null) return NotFound();
            var dto = new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                Color = car.Color,
                Price = car.Price,
                CreatedAt = car.CreatedAt,
                ModifiedAt = car.ModifiedAt
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarDto car)
        {
            if (!ModelState.IsValid) return View(car);
            await _carService.UpdateAsync(car);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _carService.GetByIdAsync(id);
            if (car == null) return NotFound();

            // Tagasta view kinnituseks
            return View(new CarDto
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _carService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
