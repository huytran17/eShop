using MISA.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class Store
    {
        #region Declare
        /// <summary>
        /// Mã duy nhất của cửa hàng trong CSDL
        /// </summary>
        public Guid StoreId { get; set; }
        /// <summary>
        /// Mã cửa hàng
        /// </summary>
        [Required("Mã cửa hàng không được để trống.")]
        public String StoreCode { get; set; } = String.Empty;
        /// <summary>
        /// Tên cửa hàng
        /// </summary>
        [Required("Tên cửa hàng không được để trống.")]
        public String StoreName { get; set; } = String.Empty;
        /// <summary>
        /// Địa chỉ cửa hàng
        /// </summary>
        [Required("Địa chỉ không được để trống.")]
        public String Address { get; set; } = String.Empty;
        /// <summary>
        /// Số điện thoại của cửa hàng
        /// </summary>
        #nullable enable
        [OnlyNumber("Số điện thoại phải là kiểu số")]
        [MaxLength(10, "Số lượng ký tự tối đa là 10")]
        public String? PhoneNumber { get; set; }
        /// <summary>
        /// Mã số thuế của cửa hàng
        /// </summary>
        public String? StoreTaxCode { get; set; }
        /// <summary>
        /// Mã quốc gia của cửa hàng
        /// Khóa ngoại với bảng quốc giá
        /// </summary>
        public Guid? CountryId { get; set; }
        /// <summary>
        /// Mã tỉnh
        /// Khóa ngoại với bảng tỉnh/thành phố
        /// </summary>
        public Guid? ProvinceId { get; set; }
        /// <summary>
        /// Mã huyện
        /// Khóa ngoại với bảng quận/huyện
        /// </summary>
        public Guid? DistrictId { get; set; }
        /// <summary>
        /// Mã phường / xã
        /// KHóa ngoại với bảng phường/ xã
        /// </summary>
        public Guid? WardId { get; set; }
        /// <summary>
        /// Đường phố
        /// </summary>
        public String? Street { get; set; }
        /// <summary>
        /// Trạng thái hoạt động
        /// 0 - Đang hoạt động
        /// 1 - Đang đóng cửa
        /// </summary>
        public StoreStatus? Status { get; set; }
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
