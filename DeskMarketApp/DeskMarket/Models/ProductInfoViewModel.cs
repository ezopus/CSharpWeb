namespace DeskMarket.Models
{
    public class ProductInfoViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public bool IsSeller { get; set; }
        public bool HasBought { get; set; }
    }
}
