using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult getAll()
        {
            //Get data from database - Domain models
            var regions = dbContext.Regions.Select(x => new RegionDTO 
            {
                Id = x.Id,
                Code = x.Code,
                FullName = x.FullName,
                RegionImageUrl = x.RegionImageUrl

            }).ToList();
            
            //Retrun DTO
            return Ok(regions);

        }

        //Get region by ID
        //GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult getById([FromRoute] Guid id)
        {
            var region = dbContext.Regions.Where(x => x.Id == id).Select(x => new RegionDTO
            {
                Id = x.Id,
                Code = x.Code,
                FullName = x.FullName,
                RegionImageUrl = x.RegionImageUrl

            }).FirstOrDefault();

            if (region is null)
            {
                return NotFound();
            }
                
            return Ok(region);
        }

        //POST region
        //POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public IActionResult Create([FromBody] CreateRegionDTO CreateRegionDTO)
        {
            //Map DTO to Model
            var regionDomainModel = new Region
            {
                Code = CreateRegionDTO.Code,
                FullName = CreateRegionDTO.FullName,
                RegionImageUrl = CreateRegionDTO.RegionImageUrl,
            };

            //Use Domain model to create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

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
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel is null)
            {
                return NotFound();
            }

            //Mad DTO to domain model

            regionDomainModel.Code = updateRegionDTO.Code;
            regionDomainModel.FullName = updateRegionDTO.FullName;
            regionDomainModel.RegionImageUrl = updateRegionDTO.RegionImageUrl;

            dbContext.SaveChanges();

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
    }
}
