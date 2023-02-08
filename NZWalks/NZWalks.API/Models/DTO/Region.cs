using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class Region
    {
        public Guid Id { get; set; } // Guid is key (primary/forign) smh ??
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }

       // Domain and DTO files can have different properties.
    }
}
