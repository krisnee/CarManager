using CarManager.Core.Dtos;
using CarManager.Core.Models;

namespace CarManager.Core.ServiceInterfaces
{
    public interface ICarService
    {
        Task<IEnumerable<CarDto>> GetAllAsync();
        Task<Car> GetByIdAsync(Guid id);
        Task<Car> CreateAsync(CarDto dto);
        Task<Car> UpdateAsync(CarDto dto);
        Task<Car> DeleteAsync(Guid id);
    }
}