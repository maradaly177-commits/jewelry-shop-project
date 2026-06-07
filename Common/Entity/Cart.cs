using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    /// <summary>
    /// Entity giỏ hàng
    /// Người thực hiện: Marada
    /// </summary>
    [Table("carts")]
    public class Cart : BaseEntity
    {
        /// <summary>
        /// ID người dùng
        /// </summary>
        public int UserId { get; set; }
    }
}