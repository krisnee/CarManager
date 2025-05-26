using System.ComponentModel.DataAnnotations;

namespace CarManager.Core.Dto
{
    public class CarDto
    {
        public int Id { get; set; }

        [Required]
        public string Make { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        [Range(1886, 9999)]
        public int Year { get; set; }

        [Required]
        public string Color { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
