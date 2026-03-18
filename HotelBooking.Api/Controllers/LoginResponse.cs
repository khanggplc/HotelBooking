namespace HotelBooking.Api.Controllers
{
    internal class LoginResponse
    {
        public string Token { get; set; }
        public string FullName { get; internal set; }
        public string AccessToken { get; internal set; }
        public string Role { get; internal set; }
        public string Email { get; internal set; }
    }
}