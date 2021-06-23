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
    public class WardRepository : BaseRepository<Ward>, IWardRepository
    {
        #region Constructor
        public WardRepository(IConfiguration configuration) : base (configuration)
        {

        }

        public List<Ward> GetByReferenceId(Guid districtId)
        {
            DynamicParameters parameters = new DynamicParameters();
            //Build câu truy vấn
            var sqlCommand = "SELECT * FROM Ward WHERE DistrictId = @DistrictId";
            parameters.Add("@DistrictId", districtId);
            //Thực thi câu truy vấn
            var wards = DbConneciton.Query<Ward>(sqlCommand, parameters).ToList();
            //Trả về kết quả
            return wards;
        }
        #endregion
    }
}
