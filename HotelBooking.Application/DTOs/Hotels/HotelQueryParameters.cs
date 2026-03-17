namespace HotelBooking.Application.DTOs.Hotels;

public class HotelQueryParameters
{
    public string? Keyword { get; set; }  // tìm theo tên/thành phố
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}