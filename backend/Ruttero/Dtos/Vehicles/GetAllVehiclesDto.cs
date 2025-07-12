namespace Ruttero.Dtos.Vehicles
{
    public class GetAllVehiclesDto
    {
        public int Id { get; set; }
        public string? LicensePlate { get; set; }
        public string? Model { get; set; }
        public string? Brand { get; set; }
    }
}