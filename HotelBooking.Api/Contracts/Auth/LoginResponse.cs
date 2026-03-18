namespace HotelBooking.Api.Contracts.Auth;

public class LoginResponse
{
    public string AccessToken { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string FullName { get; set; } = default!;
}
