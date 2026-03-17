namespace HotelBooking.Application.Abstractions.Security;

public interface IJwtTokenService
{
    string GenerateToken(string username, string role);
}