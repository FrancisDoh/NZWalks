namespace NZWalks.API.Models.DTO
{
    // Request coming from a client to use RegionsController.cs / AddRegionAsync() would look
    // just like this to avoid asking the client to send the Guid Id of the region object to be
    // added to the DB.
    public class AddRegionRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public long Population { get; set; }
    }
}
