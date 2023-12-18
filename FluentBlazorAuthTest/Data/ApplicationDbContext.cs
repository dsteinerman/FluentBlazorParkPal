using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FluentBlazorAuthTest.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace FluentBlazorAuthTest.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<FluentBlazorAuthTest.Data.Models.Space> Space { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Model configuration for Space
            modelBuilder.Entity<Space>(entity =>
            {
                // Setting precision for Latitude
                entity.Property(e => e.Latitude).HasPrecision(9, 6);

                // Setting precision for Longitude
                entity.Property(e => e.Longitude).HasPrecision(9, 6);

                // Additional configurations for Space can be added here
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),

            });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),

            });



            // Other model configurations can be added here
        }

    }
}
