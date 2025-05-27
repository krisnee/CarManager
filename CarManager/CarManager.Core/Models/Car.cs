
namespace CarManager.Core.Models;

public class Car
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int Year { get; set; }
    public string? Color { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
}
