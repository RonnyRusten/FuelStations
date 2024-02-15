namespace DrivstoffappenStations.Models
{
    public class FuelPrice
    {
        public int Id { get; set; }
        public int FuelTypeId { get; set; }
        public string Currency { get; set; }
        public decimal price { get; set; }
        public long LastUpdated { get; set; }
        public string CreatedAt { get; set; }
        public int Deleted { get; set; }
    }
}
