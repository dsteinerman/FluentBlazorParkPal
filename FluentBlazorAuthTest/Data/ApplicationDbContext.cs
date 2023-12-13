using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FluentBlazorAuthTest.Data.Models;

namespace FluentBlazorAuthTest.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<FluentBlazorAuthTest.Data.Models.Space> Space { get; set; } = default!;
    }
}
