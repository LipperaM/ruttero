namespace Ruttero.Dtos.Vehicles
{
    public class CreateVehicleRequestDto
    {
        public string LicensePlate { get; set; } = null!;

        public string? Model { get; set; }

        public string? Brand { get; set; }
    }
}