using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repositories
{
    public interface IDistrictRepository : IBaseRepository<District>
    {
        /// <summary>
        /// Lấy ra quận/huyện theo khóa ngoại
        /// </summary>
        /// <param name="provinceId">Khóa ngoại của bảng tham chiếu (ProvinceId)</param>
        /// <returns>Danh sách các quận/huyện</returns>
        /// Created By: TVHUY (16-06-2021)
        List<District> GetByReferenceId(Guid provinceId);
    }
}
