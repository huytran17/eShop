using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repositories
{
    public interface IWardRepository : IBaseRepository<Ward>
    {
        /// <summary>
        /// Lấy ra phường/xã theo khóa ngoại
        /// </summary>
        /// <param name="districtId">Khóa ngoại của bảng tham chiếu (DistrictId)</param>
        /// <returns>Danh sách các phường/xã</returns>
        /// Created By: TVHUY (16-06-2021)
        List<Ward> GetByReferenceId(Guid districtId);
    }
}
