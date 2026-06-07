using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Thực thể nhân viên
    /// Người thực hiện: Marada
    /// </summary>
    [Table("employees")]
    public class Employee : BaseEntity
    {
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required]
        [MaxLength(255)]
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}