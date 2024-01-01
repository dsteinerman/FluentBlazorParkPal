using Bogus;
using FluentBlazorAuthTest.Data;
using System;
using System.Linq;
using CSharpVitamins;

public static class DatabaseSeeder
{
    public static void SeedSpaces(ApplicationDbContext context, int count = 25)
    {
      //  if (!context.Spaces.Any())
        {
            var spaceFaker = new Faker<Space>()
                .RuleFor(s => s.Id, f => ShortGuid.NewGuid().ToString()) // Short GUID
                .RuleFor(s => s.HostId, f => null) // Assuming HostId can be null for seeding
                .RuleFor(s => s.Latitude, f => (decimal?)Math.Round(f.Address.Latitude(), 6))
                .RuleFor(s => s.Longitude, f => (decimal?)Math.Round(f.Address.Longitude(), 6))
                .RuleFor(s => s.Price, f => Math.Round(f.Random.Decimal(50, 500), 2))
                .RuleFor(s => s.Size, f => f.PickRandom<SpaceSize>())
                .RuleFor(s => s.Description, f => f.Lorem.Paragraph())
                .RuleFor(s => s.IsPublic, f => f.Random.Bool())
                .RuleFor(s => s.DateCreated, DateTime.UtcNow)
                .RuleFor(s => s.LatestTransaction, f => f.Date.Recent(30))
                .RuleFor(s => s.Bookings, new HashSet<Booking>()); // Empty bookings

            var spaces = spaceFaker.Generate(count);
          //  context.Spaces.AddRange(spaces);
         //   context.SaveChanges();
        }
    }
}