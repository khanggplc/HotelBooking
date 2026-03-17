using HotelBooking.Application.Common;
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

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<HotelDto>> Update(Guid id, [FromBody] UpdateHotelDto request, CancellationToken cancellationToken)
    {
        var hotel = new Hotel
        {
            Id = id,
            Name = request.Name,
            Address = request.Address,
            City = request.City,
            Description = request.Description ?? string.Empty
        };

        var updated = await _hotelRepository.UpdateAsync(hotel, cancellationToken);
        if (!updated) return NotFound();

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

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _hotelRepository.DeleteAsync(id, cancellationToken);
        if (!deleted) return NotFound();

        return NoContent();
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<HotelDto>>> GetAll(
    [FromQuery] HotelQueryParameters query,
    CancellationToken cancellationToken)
    {
        query.PageNumber = query.PageNumber <= 0 ? 1 : query.PageNumber;
        query.PageSize = query.PageSize <= 0 ? 10 : query.PageSize;
        query.PageSize = query.PageSize > 100 ? 100 : query.PageSize;

        var pagedHotels = await _hotelRepository.GetPagedAsync(query, cancellationToken);

        var result = new PagedResult<HotelDto>
        {
            PageNumber = pagedHotels.PageNumber,
            PageSize = pagedHotels.PageSize,
            TotalCount = pagedHotels.TotalCount,
            Items = pagedHotels.Items.Select(h => new HotelDto
            {
                Id = h.Id,
                Name = h.Name,
                Address = h.Address,
                City = h.City,
                Description = h.Description
            }).ToList()
        };

        return Ok(result);
    }
}