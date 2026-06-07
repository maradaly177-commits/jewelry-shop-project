using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cosmetic_App.Common.DTO
{
    public class AddToCartRequest
    {
        [Required]
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [Required]
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 100)]
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; } = 1;
    }
}