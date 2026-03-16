using HotelBooking.Application.DTOs.Hotels;
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
}