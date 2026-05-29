using System.ComponentModel.DataAnnotations;

namespace Cosmetic_App.Common.Entity
{
    public class User : BaseEntity
    {
        [Key]
        // Giữ lại vì BaseEntity không chứa Id
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(191)]
        public string Email { get; set; } = string.Empty;

        [Required]
        // Phải khớp chính xác tên cột PasswordHash trong database
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string? FullName { get; set; }

        [MaxLength(50)]
        // Bỏ giá trị mặc định để Dapper map chính xác giá trị từ DB (Admin/Customer)
        public string? Role { get; set; }
    }
}