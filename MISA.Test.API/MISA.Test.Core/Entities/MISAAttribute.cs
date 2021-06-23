using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    #region Class: Required
    /// <summary>
    /// Bắt buộc nhập
    /// </summary>
    class Required : Attribute
    {
        #region Declare
        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        public string Msg { get; set; }
        #endregion

        #region Constructor
        public Required(string msg = "")
        {
            Msg = msg;
        }
        #endregion
    }
    #endregion

    #region Class: OnlyNumber
    /// <summary>
    /// Kiểm tra có là kiểu số hay không
    /// </summary>
    class OnlyNumber : Attribute
    {
        #region Declare
        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        public String Msg { get; set; }
        /// <summary>
        /// Chuỗi cần kiểm tra
        /// </summary>
        public String StrNum { get; set; }
        #endregion

        #region Constructor
        public OnlyNumber(String strNum, String msg = "")
        {
            StrNum = strNum;
            Msg = msg;
        }
        #endregion
    }
    #endregion

    #region Class: MaxLength
    /// <summary>
    /// Giới hạn ký tự tối đa
    /// </summary>
    class MaxLength : Attribute
    {
        #region Declare
        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// Số lượng ký tự tối đa cho phép
        /// </summary>
        public int Max { get; set; }
        #endregion

        #region Constructor
        public MaxLength(int max, string msg = "")
        {
            Max = max;
            Msg = msg;
        }
        #endregion
    }
    #endregion
}
