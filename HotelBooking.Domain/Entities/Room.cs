using HotelBooking.Domain.Enums;

namespace HotelBooking.Domain.Entities;

public class Room
{
    public Guid Id { get; set; }
    public Guid HotelId { get; set; }
    public string RoomNumber { get; set; } = default!;
    public int Capacity { get; set; }
    public decimal PricePerNight { get; set; }
    public RoomStatus Status { get; set; } = RoomStatus.Available;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public Hotel Hotel { get; set; } = default!;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}