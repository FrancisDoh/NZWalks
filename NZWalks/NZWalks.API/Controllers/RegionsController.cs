using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories;
using System.Security.Principal;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")] // Access route will automatically be "/Regions"
    public class RegionsController : Controller
    {
        // Obect + constructor
        //RegionRepository
        private readonly IRegionRepository regionRepository;
        // Auto mapper (automatically converts models to dto).
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        // Method GetAllRegins() implementation
        [HttpGet] // Get http protocol request
        public async Task<IActionResult> GetAllRegions()
        {
            /*
             * Static data
             * 
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Welllington",
                    Code = "WLG",
                    Area = 227755,
                    Lat = -1.8822,
                    Long = 299.88,
                    Population = 500000
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland",
                    Code = "AUCK",
                    Area = 227755,
                    Lat = -1.8822,
                    Long = 299.88,
                    Population = 500000
                },
            };
            */

            // Domain var
            var regions = await regionRepository.GetAllAsync(); // This is coming from the DB.

            // DTO var
            /* var regionsDTO = new List<Models.DTO.Region>();

             regions.ToList().ForEach(region => 
             {

                 var regionDTO = new Models.DTO.Region()
                 {
                     Id = region.Id,
                     Code = region.Code,
                     Name = region.Name,
                     Area = region.Area,
                     Lat = region.Lat,
                     Long = region.Long,
                     Population = region.Population,
                 };
                 // Add each single region to the list regionsDTO
                 regionsDTO.Add(regionDTO);

                 });
            */

            var regionsDTO = mapper.Map<List<Models.DTO.Region>>(regions); // List of DTOs models from DB->Models->Profiles
            
            // Return response
            return Ok(regions); // OK returns a 200 status.
        }
    }
}
