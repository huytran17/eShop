using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Services
{
    public interface IBaseService<MISAEntity>
    {
        #region Declare
        /// <summary>
        /// Thêm mới một đối tượng MISAEntity vào CSDL
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Đối tượng chứa kết quả thực thi</returns>
        /// Created by: TVHUY (11-06-2021)
        ServiceResult Insert(MISAEntity entity);

        /// <summary>
        /// Cập nhật 1 đối tượng MISAEntity theo Id
        /// </summary>
        /// <param name="entity">Đối tượng chứa dữ liệu cần cập nhật</param>
        /// <param name="entityId">Id của đối tượng được cập nhật</param>
        /// <returns>Đối tượng chứa kết quả thực thi</returns>
        /// Crated by: TVHUY (11-06-2021)
        ServiceResult Update(MISAEntity entity, Guid entityId);

        /// <summary>
        /// Xóa 1 đối tượng MISAEntity khỏi CSDL
        /// </summary>
        /// <param name="entityId">Id của đối tượng cần xóa</param>
        /// <returns>Đối tượng chứa kết quả thực thi</returns>
        /// Created by: TVHUY (11-06-2021)
        ServiceResult Delete(Guid entityId);
        #endregion
    }
}
