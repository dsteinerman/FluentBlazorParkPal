using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace FluentBlazorAuthTest.Data.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        // private readonly ApplicationDbContext _context;_dbContextFactory;
        public SpaceService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _contextFactory = dbContextFactory;
        }
        public async Task AddSpaceAsync(Space space)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Spaces.Add(space);
            await context.SaveChangesAsync();

        }
        public Task<Space> CreateSpaceAsync(Space space)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSpaceAsync(string spaceId)
        {
            using var context = _contextFactory.CreateDbContext();
            var space = await context.Spaces.FindAsync(spaceId);
            if (space != null)
            {
                context.Spaces.Remove(space);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Space>> GetAllSpacesAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Spaces.ToListAsync();
        }

        public async Task<Space> GetSpaceByIdAsync(string spaceId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Spaces.FindAsync(spaceId);
        }

        public Task<decimal> GetSpacePriceByIdAsync(string spaceId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSpaceAsync(Space space, string spaceId)
        {
            using var context = _contextFactory.CreateDbContext();
            var dbSpace = await context.Spaces.FindAsync(spaceId);
            if (dbSpace != null)
            {
                dbSpace.Size = space.Size;
                dbSpace.Longitude = space.Longitude;
                dbSpace.Latitude = space.Latitude;
                dbSpace.Price = space.Price;
                dbSpace.Description = space.Description;
                dbSpace.Address = space.Address;
                dbSpace.Id = space.Id;
                dbSpace.IsAvailable = space.IsAvailable;
                dbSpace.IsPublic = space.IsPublic;
                dbSpace.IsVacant = space.IsVacant;

                context.Entry(dbSpace).CurrentValues.SetValues(space);
                await context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Space>> GetSpacesByHostIdAsync(string hostId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Spaces
                        .Where(s => s.HostId == hostId)
                        .ToListAsync();
        }

        async Task<IEnumerable<Space>> ISpaceService.GetSpacesByHostIdAsync(string hostId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Spaces
                        .Where(s => s.HostId == hostId)
                        .ToListAsync();
        }


        public async Task<(IEnumerable<Space>, int)> GetSpacesPageAsync(int pageNumber, int pageSize, bool includeNonPublicSpaces)
        {
            using var context = _contextFactory.CreateDbContext();
            var query = context.Spaces.AsQueryable();

            if (!includeNonPublicSpaces)
            {
                query = query.Where(space => space.IsPublic);
            }

            var totalSpaces = await query.CountAsync();
            var spaces = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (spaces, totalSpaces);
        }

        public async Task<(IEnumerable<Space>, int)> GetHostSpacesPageAsync(int pageNumber, int pageSize, ApplicationUser currentUser)
        {
            using var context = _contextFactory.CreateDbContext();

            var query = context.Spaces.Where(space => space.HostId == currentUser.Id);

            var totalSpaces = await query.CountAsync();
            var spaces = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (spaces, totalSpaces);
        }

    }
}
