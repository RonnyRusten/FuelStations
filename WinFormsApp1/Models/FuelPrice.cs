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
        public string FuelType
        {
            get
            {
                if (FuelTypeId == 1)
                {
                    return "Diesel";
                }
                if (FuelTypeId == 2)
                {
                    return "95";
                }
                if (FuelTypeId == 3)
                {
                    return "98";
                }
                return FuelTypeId.ToString();
            }
        }

        public DateTime Updated
        {
            get
            {
                //var dateTime = DateTime.FromBinary(LastUpdated);
                var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dateTime = dateTime.AddMilliseconds(LastUpdated).ToLocalTime();
                return dateTime;
            }
        }
    }
}
