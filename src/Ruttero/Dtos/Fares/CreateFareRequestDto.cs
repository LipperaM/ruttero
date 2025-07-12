namespace Ruttero.Dtos.Fares
{
    public class CreateFareRequestDto
    {

        public required string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}