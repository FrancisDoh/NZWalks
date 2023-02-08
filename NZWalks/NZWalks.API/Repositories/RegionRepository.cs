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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid(); // Auto assign a primary key (string) to the region incoming object
            await nZWalksDbContext.Regions.AddAsync(region); // .Add(object) is the regular version of auto add method.

           await  nZWalksDbContext.SaveChangesAsync(); // save changes.

            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            // Else Delete
            nZWalksDbContext.Regions.Remove(region);
            await nZWalksDbContext.SaveChangesAsync();
            // Return the deleted region, in case the client want to do something with it.
            return region; 
        }

        // Get all regions service implementation
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            
            return await nZWalksDbContext.Regions.ToListAsync(); // Returns list of regions from the DB.
        }

        // Get a single region based of its id
        public async Task<Region> GetAsync(Guid id)
        {

            return await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); // x.Id (client request Id), id (Id from the DB)
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            // Fetch region object from DB based of client request Id
            var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            
            // In case nothing found from the DB, return null to client
            if (existingRegion == null)
            {
                return null;
            }

            // Else, update found/existingRegion with new client input object attributes.
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.Area = region.Area;
            existingRegion.Lat  = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;

            // Save changes in DbContext (commit changes to the DB).
            await nZWalksDbContext.SaveChangesAsync();

            return existingRegion;

        }
    }
}
