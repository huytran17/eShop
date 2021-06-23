using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    public class ServiceResult
    {
        #region Declare
        /// <summary>
        /// Có hợp lệ hay không
        /// </summary>
        public bool IsValid { get; set; } = true;

        /// <summary>
        /// Các thông báo lỗi
        /// </summary>
        public List<String> Messages { get; set; } = new List<String>();

        /// <summary>
        /// Dữ liệu lỗi
        /// </summary>
        public object Data { get; set; }
        #endregion

        #region Methods
        public void SetMessage(String msg)
        {
            Messages.Add(msg);
        }
        #endregion
    }
}
