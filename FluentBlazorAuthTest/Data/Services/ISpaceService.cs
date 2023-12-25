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

    Task UpdateSpaceAsync(Space space);

    /* Delete */

    Task DeleteSpaceAsync(string spaceId);

}