using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        // Create object + constructor to fetch from the DB
        // nZWalksDbContext 
        private readonly NZWalksDbContext nZWalksDbContext;
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        // GetAll() service implementation
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            
            return await nZWalksDbContext.Regions.ToListAsync(); // Returns list of regions from the DB.
        }
    }
}
