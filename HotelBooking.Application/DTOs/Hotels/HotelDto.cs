namespace HotelBooking.Application.DTOs.Hotels;

public class HotelDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;
    public string? Description { get; set; } = default!;
}