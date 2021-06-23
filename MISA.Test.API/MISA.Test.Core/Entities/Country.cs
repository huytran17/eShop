using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Country
    {
        #region Declare
        /// <summary>
        /// Khóa chính
        /// </summary>
        public Guid CountryId { get; set; }
        /// <summary>
        /// Tên quốc gia
        /// </summary>
        public String CountryName { get; set; }

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
