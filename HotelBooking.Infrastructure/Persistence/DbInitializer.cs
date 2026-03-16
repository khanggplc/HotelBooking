using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();
        //Tránh trùng dữ liệu khi chạy lại từ đầu
        if (await context.Hotels.AnyAsync()) return;

        // 1) Users
        var user1 = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Nguyen Van A",
            Email = "user1@gmail.com",
            PasswordHash = "hashed_password_demo",
            Role = "User"
        };

        var admin = new User
        {
            Id = Guid.NewGuid(),
            FullName = "Admin",
            Email = "admin@gmail.com",
            PasswordHash = "hashed_password_admin",
            Role = "Admin"
        };

        // 2) Hotels
        var hotel1 = new Hotel
        {
            Id = Guid.NewGuid(),
            Name = "Sunrise Hotel",
            Address = "123 Tran Hung Dao",
            City = "Da Nang",
            Description = "Near beach"
        };

        var hotel2 = new Hotel
        {
            Id = Guid.NewGuid(),
            Name = "Moonlight Hotel",
            Address = "45 Le Loi",
            City = "Ho Chi Minh",
            Description = "City center"
        };

        // 3) Rooms
        var rooms = new List<Room>
        {
            new Room
            {
                Id = Guid.NewGuid(),
                HotelId = hotel1.Id,
                RoomNumber = "101",
                Capacity = 2,
                PricePerNight = 800000,
                Status = RoomStatus.Available
            },
            new Room
            {
                Id = Guid.NewGuid(),
                HotelId = hotel1.Id,
                RoomNumber = "102",
                Capacity = 3,
                PricePerNight = 1200000,
                Status = RoomStatus.Available
            },
            new Room
            {
                Id = Guid.NewGuid(),
                HotelId = hotel2.Id,
                RoomNumber = "201",
                Capacity = 2,
                PricePerNight = 900000,
                Status = RoomStatus.Available
            },
            new Room
            {
                Id = Guid.NewGuid(),
                HotelId = hotel2.Id,
                RoomNumber = "202",
                Capacity = 4,
                PricePerNight = 1500000,
                Status = RoomStatus.Maintenance
            }
        };

        await context.Users.AddRangeAsync(user1, admin);
        await context.Hotels.AddRangeAsync(hotel1, hotel2);
        await context.Rooms.AddRangeAsync(rooms);

        await context.SaveChangesAsync();
    }
}