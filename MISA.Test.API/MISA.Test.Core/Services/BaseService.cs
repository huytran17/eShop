using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using MISA.Core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class BaseService<MISAEntity> : IBaseService<MISAEntity>
    {
        #region Declare
        IBaseRepository<MISAEntity> _baseRepository;
        ServiceResult _serviceResult;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<MISAEntity> baseRepository)
        {
            _baseRepository = baseRepository;
            _serviceResult = new ServiceResult();
        }
        #endregion

        #region Methods
        public ServiceResult Delete(Guid entityId)
        {
            var rowEffected = _baseRepository.Delete(entityId);
            //Nếu xóa thành công
            if (rowEffected > 0)
            {
                //Build thông báo trả về
                _serviceResult.SetMessage("Xóa thành công");
            }
            else
            {
                //Build thông báo trả về
                _serviceResult.SetMessage("Không có bản ghi nào được xóa, vui lòng thử lại sau");
            }
            _serviceResult.Data = new
            {
                rowEffected = rowEffected,
            };
            //Trả về kết quả
            return _serviceResult;
        }

        public ServiceResult Insert(MISAEntity entity)
        {
            //Validate
            var isValid = ValidateObject(entity);
            //Nếu hợp lệ
            if (isValid == true)
            {
                var rowEffected = _baseRepository.Insert(entity);
                //Nếu thêm thành công
                if (rowEffected > 0)
                {
                    //Build thông báo trả về
                    _serviceResult.SetMessage("Thêm mới thành công");
                    
                }
                else
                {
                    //Build thông báo trả về
                    _serviceResult.SetMessage("Không có bản ghi nào được thêm mới, vui lòng thử lại sau");
                }
                _serviceResult.Data = new
                {
                    rowEffected = rowEffected
                };
            }
            
            //Trả về kết quả
            return _serviceResult;
        }

        public ServiceResult Update(MISAEntity entity, Guid entityId)
        {
            //Validate đối tượng cần thêm mới
            var isValid = ValidateObject(entity);

            //Nếu dữ liệu hơp lệ
            if (isValid == true)
            {
                var rowEffected = _baseRepository.Update(entity, entityId);
                //Nếu thêm thành công
                if (rowEffected > 0)
                {
                    //Build thông báo trả về
                    _serviceResult.SetMessage("Cập nhật thành công");
                }
                else
                {
                    //Build thông báo trả về
                    _serviceResult.SetMessage("Không có bản ghi nào được cập nhật, vui lòng thử lại sau");
                }
                _serviceResult.Data = new
                {
                    rowEffected = rowEffected
                };
            }
            //Trả về kết quả
            return _serviceResult;
        }

        /// <summary>
        /// Validate dữ liệu của đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần kiểm tra</param>
        /// <returns>true: hợp lệ; false: không hợp lệ</returns>
        /// Created by: TVHUY (11-06-2021)
        public bool ValidateObject(MISAEntity entity)
        {
            var isValid = true;
            //Xử lý validate chung:
            //Lấy tất cả thuộc tính của đối tượng MISAEntity
            var properties = entity.GetType().GetProperties();

            foreach (var prop in properties)
            {
                //Lấy tất cả thuộc tính được đánh dấu Required (bắt buộc nhập)
                var propRequired = prop.GetCustomAttributes(typeof(Required), true);
                //Lấy tất cả thuộc tính được đánh dấu MaxLength (Giới hạn số lượng ký tự tối đa)
                var propMaxLength = prop.GetCustomAttributes(typeof(MaxLength), true);
                //Lấy tất cả thuộc tính được đánh dấu OnlyNumber (chỉ cho phép kiểu số)
                var propOnlyNumber = prop.GetCustomAttributes(typeof(OnlyNumber), true);

                //Lấy tên và giá trị của thuộc tính
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);

                #region Required
                //Kiểm tra các thuộc tính bắt buộc nhập
                if (propRequired.Length > 0)
                {
                    //Lấy các thuộc tính được truyền vào lớp Required
                    var msg = (propRequired[0] as Required).Msg;

                    if (propValue.ToString() == string.Empty)
                    {
                        isValid = false;
                        _serviceResult.IsValid = false;

                        if (msg == string.Empty)
                        {
                            //Build thông báo lỗi
                            _serviceResult.SetMessage(String.Format("Trường {0} không được phép để trống", propName));
                            _serviceResult.Data = new
                            {
                                errorCode = ResultStatus.BadRequest,
                                errorStatus = "Bad Request"
                            };
                        }
                        else
                            _serviceResult.SetMessage(msg);
                    }
                }
                #endregion

                #region MaxLength
                //Kiểm tra các thuộc tính có giới hạn số lượng ký tự
                if (propMaxLength.Length > 0)
                {
                    //Lấy các thuộc tính được truyền vào lớp Maxlength
                    var msg = (propMaxLength[0] as MaxLength).Msg;
                    var max = (propMaxLength[0] as MaxLength).Max;

                    if (propValue.ToString().Length > max)
                    {
                        isValid = false;
                        _serviceResult.IsValid = false;

                        if (msg == string.Empty)
                        {
                            //Build thông báo lỗi
                            _serviceResult.SetMessage(String.Format("Trường {0} không được vượt quá {1} ký tự", propName, max));
                            _serviceResult.Data = new
                            {
                                errorCode = ResultStatus.BadRequest,
                                errorStatus = "Bad Request"
                            };
                        }
                        else
                            _serviceResult.SetMessage(msg);
                    }
                }
                #endregion

                #region OnlyNumber
                //Kiểm tra các thuộc tính có giới hạn số lượng ký tự
                if (propOnlyNumber.Length > 0)
                {
                    //Lấy các thuộc tính được truyền vào lớp Maxlength
                    var msg = (propOnlyNumber[0] as OnlyNumber).Msg;
                    var strNum = (propOnlyNumber[0] as OnlyNumber).StrNum;

                    if (int.TryParse(propValue.ToString(), out int n) == false)
                    {
                        isValid = false;
                        _serviceResult.IsValid = false;

                        if (msg == string.Empty)
                        {
                            //Build thông báo lỗi
                            _serviceResult.SetMessage(String.Format("Trường {0} chỉ cho phép dữ liệu là số", propName));
                            _serviceResult.Data = new
                            {
                                errorCode = ResultStatus.BadRequest,
                                errorStatus = "Bad Request"
                            };
                        }
                        else
                            _serviceResult.SetMessage(msg);
                    }
                }
                #endregion
            }
            return isValid;
        }

        #endregion
    }
}
