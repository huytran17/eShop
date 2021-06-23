using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class District
    {
        #region Declare
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid DistrictId { get; set; }
        /// <summary>
        /// Tên quận/huyện
        /// </summary>
        public String DistrictName { get; set; }

#nullable enable
        /// <summary>
        /// Khóa ngoại vào bảng tỉnh/thành phố
        /// </summary>
        public Guid ProvinceId { get; set; }
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
