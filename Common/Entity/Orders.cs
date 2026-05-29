namespace Cosmetic_App.Common.Entity
{
    public class Orders
    {
        public int Id { get; set; }
        public int? UserId { get; set; } // Liên kết với bảng users
        public decimal TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = "CART"; // Mặc định là CART
    }
}
