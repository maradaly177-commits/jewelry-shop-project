namespace Cosmetic_App.Common.DTO
{
    public class CheckoutRequest
    {
        public int UserId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}