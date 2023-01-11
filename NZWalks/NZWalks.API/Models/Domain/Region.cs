using System.Runtime;

namespace NZWalks.API.Models.Domain
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

        // Navigation Property
        public IEnumerable<Walk> Walks { get; set; } // One Region can have 1 or multiple Walk in it.

    }
}
