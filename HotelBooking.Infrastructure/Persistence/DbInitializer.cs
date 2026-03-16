using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Infrastructure.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.Hotels.AnyAsync()) return;

        var user1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var adminId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var hotel1Id = Guid.Parse("045e1399-7656-4be0-bc6e-a3e244ef5c27");
        var hotel2Id = Guid.Parse("8c941f20-e689-4aa2-a76b-4daf30a742cc");

        // 1) Users
        var user1 = new User
        {
            Id = user1Id,
            FullName = "Nguyen Van A",
            Email = "user1@gmail.com",
            PasswordHash = "hashed_password_demo",
            Role = "User"
        };

        var admin = new User
        {
            Id = adminId,
            FullName = "Admin",
            Email = "admin@gmail.com",
            PasswordHash = "hashed_password_admin",
            Role = "Admin"
        };

        // 2) Hotels
        var hotel1 = new Hotel
        {
            Id = hotel1Id,
            Name = "Sunrise Hotel",
            Address = "123 Tran Hung Dao",
            City = "Da Nang",
            Description = "Near beach"
        };

        var hotel2 = new Hotel
        {
            Id = hotel2Id,
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
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
                HotelId = hotel1Id,
                RoomNumber = "101",
                Capacity = 2,
                PricePerNight = 800000,
                Status = RoomStatus.Available
            },
            new Room
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
                HotelId = hotel1Id,
                RoomNumber = "102",
                Capacity = 3,
                PricePerNight = 1200000,
                Status = RoomStatus.Available
            },
            new Room
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
                HotelId = hotel2Id,
                RoomNumber = "201",
                Capacity = 2,
                PricePerNight = 900000,
                Status = RoomStatus.Available
            },
            new Room
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
                HotelId = hotel2Id,
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