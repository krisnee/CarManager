using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CarManager.Core.Dtos
{
    public class CarDto
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Range(0.01, 999999999, ErrorMessage = "Year must be positive between 1886 and 9999 (negative values are not allowed)")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Color is required")]
        public string Color { get; set; }

        [Range(0.01, 999999999, ErrorMessage = "Price must be greater than 0 (negative values are not allowed)")]
        public decimal? Price { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}