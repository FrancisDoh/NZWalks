using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    // Interface
    public interface IRegionRepository
    {
        // IEnumerable is similar to list
        Task<IEnumerable<Region>> GetAllAsync(); // Get all regions
        Task<Region> GetAsync(Guid id); // Get a single region based on its {id}
        Task<Region> AddAsync(Region region); // Add a new region object to the DB.
        Task<Region> DeleteAsync(Guid id); // Delete a single region from the DB.
        Task<Region> UpdateAsync(Guid id, Region region);// Update a single region from the DB.
    }
}
