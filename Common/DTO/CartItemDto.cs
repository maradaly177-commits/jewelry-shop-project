namespace Cosmetic_App.Common.DTO
{
    public class CartItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string ProductName { get; set; } = "";

        public decimal UnitPrice { get; set; }

        public string Image { get; set; } = "";
    }
}