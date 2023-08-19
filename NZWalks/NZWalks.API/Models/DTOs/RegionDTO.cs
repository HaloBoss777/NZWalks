namespace NZWalks.API.Models.DTOs
{
    public class RegionDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string? RegionImageUrl { get; set; }
    }

    public class CreateRegionDTO
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string? RegionImageUrl { get; set; }
    }

    public class UpdateRegionDTO
    {
        public string Code { get; set; }
        public string FullName { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
