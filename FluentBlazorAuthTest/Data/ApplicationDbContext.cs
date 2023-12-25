using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FluentBlazorAuthTest.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Space> Spaces { get; set; }

        // Other entities can be defined here

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /* Model configuration for Spaces */

            // Configuring one-to-many relationship between Space and Bookings
            modelBuilder.Entity<Space>()
                .HasMany(s => s.Bookings)
                .WithOne(b => b.Space)
                .HasForeignKey(b => b.SpaceId)
                .IsRequired(false) // A space can have many bookings, but it's not required
                .OnDelete(DeleteBehavior.SetNull); // Upon Space deletion, its associated Bookings are still retained
                                                        // Note: Considered the possibility of "archiving" Spaces instead of deleting them to retain associated data.
                                                        // However, this might lead to issues regarding user privacy.

           /* Model configuration for ApplicationUser */

           // Configuring one-to-many relationship between ApplicationUser and Spaces
           modelBuilder.Entity<ApplicationUser>()
               .HasMany(u => u.HostedSpaces)
               .WithOne(s => s.HostUser)
               .HasForeignKey(s => s.HostId)
               .IsRequired(false); // A user can have many spaces, but it's not required

           // Configuring one-to-many relationship between ApplicationUser and Bookings
           modelBuilder.Entity<ApplicationUser>()
               .HasMany(u => u.Bookings)
               .WithOne(b => b.ClientUser)
               .HasForeignKey(b => b.ClientUserId)
               .IsRequired(false); // A user can have many bookings, but it's not required

            // Other model configurations can be added here


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
