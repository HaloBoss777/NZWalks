using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {

        [HttpGet]
        public IActionResult getALl()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    FullName = "Auckland Region",
                    Code = "AKL",
                    RegionImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.aucklandcouncil.govt.nz%2Fparks-recreation%2Fget-outdoors%2Faklpaths%2FPages%2Fpath-detail.aspx%3FItemId%3D306&psig=AOvVaw1Gd553h_Bld0gB7TEEvfp6&ust=1690369078882000&source=images&cd=vfe&opi=89978449&ved=0CBEQjRxqFwoTCPjBrt7ZqYADFQAAAAAdAAAAABAE"

                },

                new Region
                {
                    Id = Guid.NewGuid(),
                    FullName = "Wellington Region",
                    Code = "WLG",
                    RegionImageUrl = "https://www.google.com/imgres?imgurl=https%3A%2F%2Fimages.myguide-cdn.com%2Fmd%2Fcontent%2F2%2Flarge%2Fbest-wellington-bike-trails-712661.jpg&tbnid=DvdxwnQM34bVvM&vet=12ahUKEwje0NOb2qmAAxWHpicCHe5EBKoQMygCegUIARC-AQ..i&imgrefurl=https%3A%2F%2Fwww.myguidewellington.com%2Ftravel-articles%2Fbest-wellington-bike-trails&docid=LsjMQ0umcqrahM&w=1200&h=600&q=wellington%20regional%20trails&ved=2ahUKEwje0NOb2qmAAxWHpicCHe5EBKoQMygCegUIARC-AQ"

                }
            };

            return Ok(regions);

        }
    }
}
