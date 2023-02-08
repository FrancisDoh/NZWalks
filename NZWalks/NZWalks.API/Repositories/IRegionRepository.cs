using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    // Interface
    public interface IRegionRepository
    {
       Task <IEnumerable<Region>> GetAllAsync(); // IEnumerable is similar to list
    }
}
