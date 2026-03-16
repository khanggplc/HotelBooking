using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Entities;
using HotelBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly AppDbContext _context;

    public HotelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Hotel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Hotels
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Room>> GetRoomsByHotelIdAsync(Guid hotelId, CancellationToken cancellationToken = default)
    {
        return await _context.Rooms
            .AsNoTracking()
            .Where(r => r.HotelId == hotelId)
            .OrderBy(r => r.RoomNumber)
            .ToListAsync(cancellationToken);
    }

    public async Task<Hotel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Hotels
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
    }

    public async Task<Hotel> AddAsync(Hotel hotel, CancellationToken cancellationToken = default)
    {
        await _context.Hotels.AddAsync(hotel, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return hotel;
    }

    public async Task<bool> UpdateAsync(Hotel hotel, CancellationToken cancellationToken = default)
    {
        var existing = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == hotel.Id, cancellationToken);
        if (existing is null) return false;

        existing.Name = hotel.Name;
        existing.Address = hotel.Address;
        existing.City = hotel.City;
        existing.Description = hotel.Description;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var hotel = await _context.Hotels.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        if (hotel is null) return false;

        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
