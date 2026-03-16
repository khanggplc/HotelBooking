using HotelBooking.Application.DTOs.Hotels;
using HotelBooking.Application.DTOs.Rooms;
using HotelBooking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HotelsController : ControllerBase
{
    private readonly IHotelRepository _hotelRepository;

    public HotelsController(IHotelRepository hotelRepository)
    {
        _hotelRepository = hotelRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<HotelDto>>> GetAll(CancellationToken cancellationToken)
    {
        var hotels = await _hotelRepository.GetAllAsync(cancellationToken);

        var result = hotels.Select(h => new HotelDto
        {
            Id = h.Id,
            Name = h.Name,
            Address = h.Address,
            City = h.City,
            Description = h.Description
        }).ToList();

        return Ok(result);
    }

    [HttpGet("{id:guid}/rooms")]
    public async Task<ActionResult<List<RoomDto>>> GetRoomsByHotelId(Guid id, CancellationToken cancellationToken)
    {
        var rooms = await _hotelRepository.GetRoomsByHotelIdAsync(id, cancellationToken);

        var result = rooms.Select(r => new RoomDto
        {
            Id = r.Id,
            RoomNumber = r.RoomNumber,
            Capacity = r.Capacity,
            PricePerNight = r.PricePerNight,
            Status = r.Status.ToString()
        }).ToList();

        return Ok(result);
    }
}