using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositries;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        //Get all regions
        //GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            //Get data from database - Domain models
            var regions = await regionRepository.GetAllAsync();

            //Model to DTO
            var regionDTO = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    FullName = region.FullName,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }
            
            //Retrun DTO
            return Ok(regions);

        }

        //Get region by ID
        //GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var region = await regionRepository.getByIdAsync(id);

            if (region is null)
            {
                return NotFound();
            }

            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                FullName = region.FullName,
                RegionImageUrl = region.RegionImageUrl,
            };


            return Ok(regionDTO);
        }

        //POST region
        //POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDTO CreateRegionDTO)
        {
            //Map DTO to Model
            var regionDomainModel = new Region
            {
                Code = CreateRegionDTO.Code,
                FullName = CreateRegionDTO.FullName,
                RegionImageUrl = CreateRegionDTO.RegionImageUrl,
            };

            //Use Domain model to create Region
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map Domain model back to DTO
            var regionDTO = new RegionDTO
            { 
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,  
                FullName = regionDomainModel.FullName,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(getById), new { id = regionDTO.Id}, regionDTO);
        }


        //Update Regions
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            // Map DTO to model
            var region = new Region 
            {
                Code = updateRegionDTO.Code,
                FullName = updateRegionDTO.FullName,
                RegionImageUrl = updateRegionDTO.RegionImageUrl,
            };

            region = await regionRepository.UpdateAsync(id, region);

            if (region is null)
            {
                return NotFound();
            };

            //Convert model to DTO
            var regionDTO = new RegionDTO
            {
                Id = region.Id,
                Code = region.Code,
                FullName = region.FullName,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(regionDTO);
        }

        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedRegion = await regionRepository.DeleteAsync(id);

            if (deletedRegion is null)
            {
                return NotFound();
            }

            var regionDTO = new RegionDTO
            {
                Id = deletedRegion.Id,
                Code = deletedRegion.Code,
                FullName = deletedRegion.FullName,
                RegionImageUrl = deletedRegion.RegionImageUrl,
            };

            return Ok(regionDTO);
        }
    }
}
