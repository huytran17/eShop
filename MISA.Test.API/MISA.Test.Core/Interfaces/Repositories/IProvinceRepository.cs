using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repositories
{
    public interface IProvinceRepository : IBaseRepository<Province>
    {
        /// <summary>
        /// Lấy ra tỉnh/thành phố theo khóa ngoại
        /// </summary>
        /// <param name="countryId">Khóa ngoại của bảng tham chiếu (CountryId)</param>
        /// <returns>Danh sách các tỉnh/thành phố</returns>
        /// Created By: TVHUY (16-06-2021)
        List<Province> GetByReferenceId(Guid countryId);
    }
}
