using System.ComponentModel.DataAnnotations;

namespace Cosmetic_App.Common.Entity
{
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        public DateTimeOffset? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        [MaxLength(255, ErrorMessage = "Người tạo tối đa 255 ký tự")]
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        [MaxLength(255, ErrorMessage = "Người sửa tối đa 255 ký tự")]
        public DateTimeOffset? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedBy: NTTrung (16/07/2023) 
        public string? ModifiedBy { get; set; }
    }
}
