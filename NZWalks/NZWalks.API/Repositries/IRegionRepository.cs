using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositries
{
    public interface IRegionRepository
    {
       Task<List<Region>> GetAllAsync();
    }
}
