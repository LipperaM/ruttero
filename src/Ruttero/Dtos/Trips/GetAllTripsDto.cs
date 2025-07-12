namespace Ruttero.Dtos.Trips
{
    public class GetAllTripsDto
    {
        public int Id { get; set; }
        public string? DriverName { get; set; }

        public string? VehiclePlate { get; set; }

        public decimal Fare { get; set; }

        public string Origin { get; set; } = null!;

        public string Destination { get; set; } = null!;

        public DateTime Date { get; set; }
    }
}