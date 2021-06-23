using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repositories
{
    public interface IStoreRepository : IBaseRepository<Store>
    {
        /// <summary>
        /// Kiểm tra bản ghi có tồn tại hay không thông qua mã cửa hàng
        /// </summary>
        /// <param name="storeCode">Mã cửa hàng - Giá trị cần kiểm tra</param>
        /// <returns>true: nếu có ít nhất 1 bản ghi tồn tại, false: nếu không có bản ghi nào tồn tại</returns>
        /// Created by: TVHUY (18-06-2021)
        bool IsExistsStoreCode(String storeCode);

        /// <summary>
        /// Phân trang dữ liệu
        /// </summary>
        /// <param name="pageSize">Số bản ghi cần lấy</param>
        /// <param name="pageIndex">Vị trí bắt đầu lấy</param>
        /// <returns>Danh sách cửa hàng, tổng số bản ghi, tổng số trang</returns>
        /// Created by: TVHUY (21-06-2021)
        (List<Store> listStores, int totalRecords, int totalPages) GetPaging(int? pageSize = 50, int? pageIndex = 1);

        #nullable enable
        /// <summary>
        /// Lọc dữ liệu có phân trang
        /// </summary>
        /// <param name="pageSize">Số bản ghi cần lấy</param>
        /// <param name="pageIndex">Vị trí bắt đầu lấy</param>
        /// <param name="storeCode">Mã cửa hàng</param>
        /// <param name="storeName">Tên cửa hàng</param>
        /// <param name="address">Địa chỉ cửa hàng</param>
        /// <param name="phoneNumber">Số điện thoại cửa hàng</param>
        /// <param name="status">Trạng thái hoạt động của cửa hàng</param>
        /// <returns>Danh sách cửa hàng, tổng số bản ghi, tổng số trang</returns>
        /// Created by: TVHUY (22-06-2021)
        (List<Store> listStores, int totalRecords, int totalPages) GetPagingFilter(int? pageSize = 50, int? pageIndex = 1, String? storeCode = "", String? storeName = "", String? address = "", String? phoneNumber = "", int? status = 2);
    }
}
