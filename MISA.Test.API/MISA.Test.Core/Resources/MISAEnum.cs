using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Resources
{
    /// <summary>
    /// Giới tính
    /// </summary>
    public enum StoreStatus
    {
        /// <summary>
        /// Đang hoạt động
        /// </summary>
        Active = 0,
        /// <summary>
        /// Ngừng hoạt động
        /// </summary>
        Unactive = 1,
    }

    /// <summary>
    /// Trạng thái của kết quả trả về
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// Sai dữ liệu
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// Lỗi server
        /// </summary>
        ServerError = 500,
        /// <summary>
        /// Không tìm thấy
        /// </summary>
        NotFound = 400,
        /// <summary>
        /// Không có kết quả
        /// </summary>
        NoContent = 204,
    }
}
