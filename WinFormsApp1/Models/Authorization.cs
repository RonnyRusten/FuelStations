namespace DrivstoffappenStations.Models
{
    public class Authorization
    {
        public int Id { get; set; }
        public int AuthorizationId { get; set; }
        public string Token { get; set; }
        public string CreatedAt { get; set; }
        public string ExpiresAt { get; set; }
        public int Deleted { get; set; }
    }
}
