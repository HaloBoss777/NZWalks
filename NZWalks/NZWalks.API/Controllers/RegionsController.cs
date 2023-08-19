using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get all regions
        //GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            //Get data from database - Domain models
            var regions = await dbContext.Regions.Select(x => new RegionDTO 
            {
                Id = x.Id,
                Code = x.Code,
                FullName = x.FullName,
                RegionImageUrl = x.RegionImageUrl

            }).ToListAsync();
            
            //Retrun DTO
            return Ok(regions);

        }

        //Get region by ID
        //GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var region = await dbContext.Regions.Where(x => x.Id == id).Select(x => new RegionDTO
            {
                Id = x.Id,
                Code = x.Code,
                FullName = x.FullName,
                RegionImageUrl = x.RegionImageUrl

            }).FirstOrDefaultAsync();

            if (region is null)
            {
                return NotFound();
            }
                
            return Ok(region);
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
            await dbContext.Regions.AddAsync(regionDomainModel);
            await dbContext.SaveChangesAsync();

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
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(regionDomainModel is null)
            {
                return NotFound();
            }

            //Mad DTO to domain model

            regionDomainModel.Code = updateRegionDTO.Code;
            regionDomainModel.FullName = updateRegionDTO.FullName;
            regionDomainModel.RegionImageUrl = updateRegionDTO.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            //Convert DTO to model
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                FullName = regionDomainModel.FullName,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDTO);
        }

        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (regionDomainModel is null)
            {
                return NotFound();
            }

            dbContext.Regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
