using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Ward
    {
        #region Declare
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid WardId { get; set; }
        /// <summary>
        /// Tên phường xã
        /// </summary>
        public String WardName { get; set; }
        /// <summary>
        /// Khóa ngoại đến bảng quận/huyện
        /// </summary>
        public Guid DistrictId { get; set; }

#nullable enable
        /// <summary>
        /// Ngày tạo bản ghi
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        public String? CreatedBy { get; set; }
        /// <summary>
        /// Ngày chỉnh sửa bản ghi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người chỉnh sửa bản ghi
        /// </summary>
        public String? ModifiedBy { get; set; }
#nullable disable
        #endregion
    }
}
