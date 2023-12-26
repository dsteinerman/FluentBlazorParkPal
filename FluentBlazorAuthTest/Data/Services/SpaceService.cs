
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
            if(space != null)
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

                await _context.SaveChangesAsync();
            }
        }
    }
}
