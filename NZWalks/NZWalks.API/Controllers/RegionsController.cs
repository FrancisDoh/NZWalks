using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
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

        // Method GetAllRegions() implementation
        [HttpGet] // Get http protocol request
        public async Task<IActionResult> GetAllRegionsAsync()
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

        // Method to return a single region
        [HttpGet]
        [Route("{id:guid}")] // id:guid restrict the route to only accept guid input
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id) 
        { 
            var region = await regionRepository.GetAsync(id); //

            var regionDTO = mapper.Map<Models.DTO.Region>(region); // source (region) converted into regionDTO (destination).

            // If region object is not found in the DB.
            if(regionDTO != null)
            {
                return NotFound();
            }
            // If region is found
            return Ok(regionDTO);
        }

        // Add a new region to the DB method
        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            // Request (DTO) to Domain Model
            var region = new Models.Domain.Region() // Domain region var
            {
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Name = addRegionRequest.Name,
                Population = addRegionRequest.Population
            };

            // Pass details to Repository
            region = await regionRepository.AddAsync(region); // Save addRegionRequest, then smash back into region var.

            // Convert back to DTO 
            var regionDTO = new Models.DTO.Region() // DTO region var format to be fetch from DB after insertion.
            {
                Id = region.Id, 
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };
            // CreatedAtAction return 201 status in case of success, and is used for insertion
            // nameof is used to privilege a type safe.
            // GetRegionAsync method is called to return the newly created region
            // new{id = regionDTO.Id} parameter to get the single region jxt created.
            // regionDTO returned format.
            return CreatedAtAction(nameof(GetRegionAsync), new{id = regionDTO.Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // Get region from the DB
            var region = await regionRepository.DeleteAsync(id);// Use repo method to delete method from the DB

            // If null NotFound
            if(region == null)
            {
                return null;
            }

            //Else convert response back to DTO
            var regionDTO = new Models.DTO.Region() // DTO region var format.
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            // return OK response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        // Region update method implementation
        // [FromRoute] & [FromBody] in param specifies the source of where id is coming from.
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            // Converts DTO to Domain model
            var region = new Models.Domain.Region() // Domain region var to be used by UpdateAync(..) repo method.
            {
                Code = updateRegionRequest.Code,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Name = updateRegionRequest.Name,
                Population = updateRegionRequest.Population
            };

            // Update Region using repository
            region = await regionRepository.UpdateAsync(id, region);

            // iff null => not found
            if(region == null)
            {
                return null;
            }

            // Else convert Domain back to DTO
            var regionDTO = new Models.DTO.Region() // DTO region var format.
            {
                Id = region.Id,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Name = region.Name,
                Population = region.Population
            };

            // Return Ok response
            return Ok(regionDTO);
        }
    }
}
