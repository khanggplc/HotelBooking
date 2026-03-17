using HotelBooking.Domain.Entities;
using HotelBooking.Application.Common;
using HotelBooking.Application.DTOs.Hotels;
namespace HotelBooking.Application.Interfaces;

public interface IHotelRepository
{
    Task<List<Hotel>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<Room>> GetRoomsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default);
    Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Hotel> AddAsync(Hotel hotel, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Hotel hotel, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PagedResult<Hotel>> GetPagedAsync(HotelQueryParameters query, CancellationToken cancellationToken = default);

}