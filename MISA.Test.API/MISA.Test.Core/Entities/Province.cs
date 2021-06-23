using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Province
    {
        #region Declare
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid ProvinceId { get; set; }

        /// <summary>
        /// Tên tỉnh/thành phố
        /// </summary>
        public String ProvinceName { get; set; }

#nullable enable
        /// <summary>
        /// Khóa ngoại đến bảng quốc gia
        /// </summary>
        public Guid CountryId { get; set; }

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
