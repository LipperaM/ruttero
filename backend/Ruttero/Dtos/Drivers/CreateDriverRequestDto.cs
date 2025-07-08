namespace Ruttero.Dtos.Drivers
{
    public class CreateDriverRequestDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string NationalId { get; set; } = null!;
    }
}