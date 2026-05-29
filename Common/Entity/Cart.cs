using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cosmetic_App.Common.Entity
{
    [Table("carts")]
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}