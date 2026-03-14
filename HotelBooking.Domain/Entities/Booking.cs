using HotelBooking.Domain.Enums;

namespace HotelBooking.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public Guid RoomId { get; set; }
    public Guid UserId { get; set; }

    public DateOnly CheckInDate { get; set; }
    public DateOnly CheckOutDate { get; set; }

    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public Room Room { get; set; } = default!;
    public User User { get; set; } = default!;
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}