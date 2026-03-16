using HotelBooking.Domain.Entities;

namespace HotelBooking.Application.Interfaces;

public interface IHotelRepository
{
    Task<List<Hotel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<Room>> GetRoomsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);
}