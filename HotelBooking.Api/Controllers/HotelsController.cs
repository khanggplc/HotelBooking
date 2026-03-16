using HotelBooking.Application.DTOs;
using HotelBooking.Application.DTOs.Hotels;
using HotelBooking.Application.DTOs.Rooms;
using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Entities;
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<HotelDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(id, cancellationToken);
        if (hotel is null) return NotFound();

        var result = new HotelDto
        {
            Id = hotel.Id,
            Name = hotel.Name,
            Address = hotel.Address,
            City = hotel.City,
            Description = hotel.Description
        };

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<HotelDto>> Create([FromBody] CreateHotelDto request, CancellationToken cancellationToken)
    {
        var hotel = new Hotel
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Address = request.Address,
            City = request.City,
            Description = request.Description ?? string.Empty
        };

        var created = await _hotelRepository.AddAsync(hotel, cancellationToken);

        var result = new HotelDto
        {
            Id = created.Id,
            Name = created.Name,
            Address = created.Address,
            City = created.City,
            Description = created.Description
        };

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}