namespace HotelBooking.Application.DTOs.Rooms;

public class RoomDto
{
    public Guid Id { get; set; }
    public string RoomNumber { get; set; } = default!;
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public string Status { get; set; } = default!;
}