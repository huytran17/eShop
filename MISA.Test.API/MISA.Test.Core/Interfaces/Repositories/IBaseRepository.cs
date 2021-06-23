using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces.Repositories
{
    public interface IBaseRepository<MISAEntity>
    {
        #region Declare
        /// <summary>
        /// Lấy hết các bản ghi
        /// </summary>
        /// <returns>Danh sách các đối tượng MISAEntity</returns>
        /// Created by: TVHUY (11-06-2021)
        List<MISAEntity> GetAll();

        /// <summary>
        /// Lấy ra đối tượng MISAEntity theo Id
        /// </summary>
        /// <param name="entityId">Id của đối tượng cần lấy dữ liệu</param>
        /// <returns>1 đối tượng MISAEntity</returns>
        /// Created By: TVHUY (11-06-2021)
        MISAEntity GetById(Guid entityId);

        /// <summary>
        /// Thêm mới một đối tượng MISAEntity vào CSDL
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số bản ghi được thêm thành công</returns>
        /// Created by: TVHUY (11-06-2021)
        int? Insert(MISAEntity entity);

        /// <summary>
        /// Cập nhật 1 đối tượng MISAEntity theo Id
        /// </summary>
        /// <param name="entity">Đối tượng chứa dữ liệu cần cập nhật</param>
        /// <param name="entityId">Id của đối tượng được cập nhật</param>
        /// <returns>Số bản ghi được cập nhật thành công</returns>
        /// Crated by: TVHUY (11-06-2021)
        int? Update(MISAEntity entity, Guid entityId);

        /// <summary>
        /// Xóa 1 đối tượng MISAEntity khỏi CSDL
        /// </summary>
        /// <param name="entityId">Id của đối tượng cần xóa</param>
        /// <returns>Số bản ghi được xóa thành công</returns>
        /// Created by: TVHUY (11-06-2021)
        int? Delete(Guid entityId);
        #endregion
    }
}
