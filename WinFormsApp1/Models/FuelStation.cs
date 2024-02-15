namespace DrivstoffappenStations.Models
{
    public class FuelStation
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int CountryId { get; set; }
        public int StationTypeId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Pending { get; set; }
        public int Deleted { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public List<FuelPrice> Prices { get; set; }

    }
}
