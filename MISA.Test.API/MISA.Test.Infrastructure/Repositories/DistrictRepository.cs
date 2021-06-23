using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        #region Constructor
        public DistrictRepository(IConfiguration configuration) : base (configuration)
        {

        }

        public List<District> GetByReferenceId(Guid provinceId)
        {
            DynamicParameters parameters = new DynamicParameters();
            //Build câu truy vấn
            var sqlCommand = "SELECT * FROM District WHERE ProvinceId = @ProvinceId";
            parameters.Add("@ProvinceId", provinceId);
            //Thực thi câu truy vấn
            var districts = DbConneciton.Query<District>(sqlCommand, parameters).ToList();
            //Trả về kết quả
            return districts;
        }
        #endregion
    }
}
