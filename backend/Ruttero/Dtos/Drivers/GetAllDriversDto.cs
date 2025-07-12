namespace Ruttero.Dtos.Drivers
{
    public class GetAllDriversDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string NationalId { get; set; } = null!;
    }
}