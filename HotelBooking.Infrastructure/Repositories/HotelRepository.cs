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
}
