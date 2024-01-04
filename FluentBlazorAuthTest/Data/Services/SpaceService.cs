using Microsoft.EntityFrameworkCore;

namespace FluentBlazorAuthTest.Data.Services
{
    public class SpaceService : ISpaceService
    {
        private readonly ApplicationDbContext _context;
        public SpaceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddSpaceAsync(Space space)
        {
            _context.Spaces.Add(space);
            await _context.SaveChangesAsync();

        }
        public Task<Space> CreateSpaceAsync(Space space)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteSpaceAsync(string spaceId)
        {
            var space = await _context.Spaces.FindAsync(spaceId);
            if (space != null)
            {
                _context.Spaces.Remove(space);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Space>> GetAllSpacesAsync()
        {
            var result = await _context.Spaces.ToListAsync();
            return result;
        }

        public async Task<Space> GetSpaceByIdAsync(string spaceId)
        {
            var result = await _context.Spaces.FindAsync(spaceId);
            return result;
        }

        public Task<decimal> GetSpacePriceByIdAsync(string spaceId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSpaceAsync(Space space, string spaceId)
        {
            var dbSpace = await _context.Spaces.FindAsync(spaceId);
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

                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Space>> GetSpacesByHostIdAsync(string hostId)
        {
            return await _context.Spaces
                        .Where(s => s.HostId == hostId)
                        .ToListAsync();
        }

        async Task<IEnumerable<Space>> ISpaceService.GetSpacesByHostIdAsync(string hostId)
        {
            return await _context.Spaces
                        .Where(s => s.HostId == hostId)
                        .ToListAsync();
        }


        public async Task<(IEnumerable<Space>, int)> GetSpacesPageAsync(int pageNumber, int pageSize, bool includeNonPublicSpaces)
        {
            var query = _context.Spaces.AsQueryable();

            if (!includeNonPublicSpaces)
            {
                query = query.Where(space => space.IsPublic);
            }

            var totalSpaces = await query.CountAsync();
            var spaces = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (spaces, totalSpaces);
        }

        public async Task<(IEnumerable<Space>, int)> GetHostSpacesPageAsync(int pageNumber, int pageSize, bool isAdmin, ApplicationUser currentUser)
        {
            var query = _context.Spaces.AsQueryable();

            if (!isAdmin)
            {
                query = query.Where(space => space.HostId == currentUser.Id);
            }

            var totalSpaces = await query.CountAsync();
            var spaces = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (spaces, totalSpaces);
        }
    }
}
