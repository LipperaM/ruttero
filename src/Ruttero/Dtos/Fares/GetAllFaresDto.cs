namespace Ruttero.Dtos.Fares
{
    public class GetAllFaresDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}