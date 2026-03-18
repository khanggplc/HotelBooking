using BCrypt.Net;
using HotelBooking.Api.Contracts.Auth;
using HotelBooking.Application.Abstractions.Security;
using HotelBooking.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly AppDbContext _dbContext;

        public AuthController(
            IJwtTokenService jwtTokenService,
            AppDbContext dbContext)
        {
            _jwtTokenService = jwtTokenService;
            _dbContext = dbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
        {
            var email =     request.Email.Trim().ToLower();

            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email, cancellationToken);

            if (user is null)
                return Unauthorized(new { message = "Invalid email or password." });

            bool isValidPassword;
            try
            {
                isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            }
            catch
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }


            var token = _jwtTokenService.GenerateToken(user.Email, user.Role);

            return Ok(new
            {
                accessToken = token,
                role = user.Role,
                email = user.Email,
                fullName = user.FullName
            });
        }
    }
}