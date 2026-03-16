using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Application.DTOs
{
    public class CreateHotelDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(300)]
        public string Address { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string City { get; set; } = default!;

        [MaxLength(1000)]
        public string? Description { get; set; }
    }
}
