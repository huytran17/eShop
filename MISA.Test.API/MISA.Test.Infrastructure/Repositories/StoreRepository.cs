using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class StoreRepository : BaseRepository<Store>, IStoreRepository
    {
        #region Constructor
        public StoreRepository(IConfiguration configuration) : base (configuration)
        {
            
        }
        #endregion
        #region Methods

#nullable enable
        public (List<Store> listStores, int totalRecords, int totalPages) GetPaging(int? pageSize = 50, int? pageIndex = 1)
        {
            //Kiểm tra dữ liệu đàu vào
            //Kiểm tra vị trí bắt đầu lấy có lớn hơn 0 và có phải là số nguyên hay không
            if (pageIndex <=0 || !int.TryParse(pageIndex.ToString(), out _))
            {
                pageIndex = 1; //Đặt giá trị mặc định
            }
            //Kiểm tra số bản ghi cần lấy có lớn hơn 0 và có phải là số nguyên hay không
            if (pageSize <= 0 || !int.TryParse(pageSize.ToString(), out _))
            {
                pageSize = 50; //Đặt giá trị mặc định
            }
            //Bind tham số cho proc phân trang
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageIndex", pageIndex);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //Thực thi lệnh phân trang
            var stores = DbConneciton.Query<Store>("Proc_GetStorePaging", parameters , commandType: CommandType.StoredProcedure).ToList();
            //Gán output của proc vào biến
            int totalRecords = parameters.Get<int>("TotalRecord");
            int totalPages = parameters.Get<int>("TotalPage");
            //Trả về kết quả (tupple dapper)
            return (stores, totalRecords, totalPages);
        }

        public (List<Store> listStores, int totalRecords, int totalPages) GetPagingFilter(int? pageSize = 50, int? pageIndex = 1, string? storeCode = "", string? storeName = "", string? address = "", string? phoneNumber = "", int? status = 2)
        {
            //Kiểm tra dữ liệu đàu vào
            //Kiểm tra vị trí bắt đầu lấy có lớn hơn 0 và có phải là số nguyên hay không
            if (pageIndex <= 0 || !int.TryParse(pageIndex.ToString(), out _))
            {
                pageIndex = 1; //Đặt giá trị mặc định
            }
            //Kiểm tra số bản ghi cần lấy có lớn hơn 0 và có phải là số nguyên hay không
            if (pageSize <= 0 || !int.TryParse(pageSize.ToString(), out _))
            {
                pageSize = 50; //Đặt giá trị mặc định
            }


            //Bind tham số cho proc phân trang
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageIndex", pageIndex);
            parameters.Add("@TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@StoreCode", storeCode);
            parameters.Add("@StoreName", storeName);
            parameters.Add("@Address", address);
            parameters.Add("@PhoneNumber", phoneNumber);
            parameters.Add("@Status", status);

            //Lấy tổng số bản ghi
            var totalRecordsCount = DbConneciton.Query<Store>("Proc_GetCountStoresWithFilter", parameters, commandType: CommandType.StoredProcedure).ToList().Count;

            parameters.Add("@Total", totalRecordsCount);

            //Thực thi lệnh phân trang và lọc dữ liệu
            var stores = DbConneciton.Query<Store>("Proc_GetPagingFilter", parameters, commandType: CommandType.StoredProcedure).ToList();
            //Gán output của proc vào biến
            int totalRecords = parameters.Get<int>("TotalRecord");
            int totalPages = parameters.Get<int>("TotalPage");
            //Trả về kết quả (tupple dapper)
            return (stores, totalRecords, totalPages);
        }

        public bool IsExistsStoreCode(string storeCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            //Buil câu truy vấn
            var sqlCommand = "SELECT * FROM Store WHERE StoreCode = @StoreCode";
            //Gán các giá trị động cho câu truy vấn
            parameters.Add("@StoreCode", storeCode);
            //Thực thi câu truy vấn
            var result = DbConneciton.Query<Store>(sqlCommand, parameters).ToList();
            //Nếu có ít nhất 1 bản ghi
            if (result.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
