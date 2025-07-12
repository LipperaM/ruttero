namespace Ruttero.Dtos.Trips
{
    public class CreateTripRequestDto
    {
        public int DriverId { get; set; }

        public int VehicleId { get; set; }

        public int FareId { get; set; }

        public string Origin { get; set; } = null!;

        public string Destination { get; set; } = null!;

        public DateTime Date { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}