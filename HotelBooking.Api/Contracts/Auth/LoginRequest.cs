using System.ComponentModel.DataAnnotations;
namespace HotelBooking.Api.Contracts.Auth;
public class LoginRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required, MinLength(6)]
    public string Password { get; set; } = default!;
}