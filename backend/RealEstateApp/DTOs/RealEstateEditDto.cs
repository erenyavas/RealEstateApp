namespace RealEstateApp.DTOs
{
    public class RealEstateEditDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CurrencyId { get; set; }

        public int RealEstateStatusId { get; set; }
    }
}
