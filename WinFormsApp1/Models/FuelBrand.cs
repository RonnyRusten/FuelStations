namespace DrivstoffappenStations.Models
{
    public class FuelBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int DisplayOrder { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public int Deleted { get; set; }
        public List<int> CountryIds { get; set; }
    }
}
