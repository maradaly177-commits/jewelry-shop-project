using System.ComponentModel.DataAnnotations;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Thực thể người dùng hệ thống
    /// Người thực hiện: Marada
    /// </summary>
    public class User : BaseEntity
    {
        [Required]
        [EmailAddress]
        [MaxLength(191)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? Role { get; set; } = "Customer";
    }
}