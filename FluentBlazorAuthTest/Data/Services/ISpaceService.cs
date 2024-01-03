using Microsoft.EntityFrameworkCore;

namespace FluentBlazorAuthTest.Data.Services;

public interface ISpaceService
{
    /* Create */

    Task<Space> CreateSpaceAsync(Space space);

    /* Read */

    Task<Space> GetSpaceByIdAsync(string spaceId);

    // Can narrow down search criteria by invoking LINQ queries
    Task<IEnumerable<Space>> GetAllSpacesAsync();

    /* Update */

    Task UpdateSpaceAsync(Space space, string spaceId);

    /* Delete */

    Task DeleteSpaceAsync(string spaceId);

    Task AddSpaceAsync(Space space);

    Task<IEnumerable<Space>> GetSpacesByHostIdAsync(string hostId);

    Task<decimal> GetSpacePriceByIdAsync(string spaceId);

    Task<(IEnumerable<Space>, int)> GetSpacesPageAsync(int pageNumber, int pageSize);


}