namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public double Length { get; set; }
        public Guid Region { get; set; }
        public Guid WalkDifficulty { get; set; }

        // Navigation properties
        //public Region? Region { get; set; } // ? stands for nullable.
        //public WalkDifficulty? WalkDifficulty { get; set; }

        // New version
        //public Region? Region { get; set; } // ? stands for nullable.
        //public WalkDifficulty? WalkDifficulty { get; set; }
    }
}
