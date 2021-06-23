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
    public class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
    {
        #region Constructor
        public ProvinceRepository(IConfiguration configuration) : base (configuration)
        {

        }

        public List<Province> GetByReferenceId(Guid countryId)
        {
            DynamicParameters parameters = new DynamicParameters();
            //Build câu truy vấn
            var sqlCommand = "SELECT * FROM Province WHERE CountryId = @CountryId";
            parameters.Add("@CountryId", countryId);
            //Thực thi câu truy vấn
            var provinces = DbConneciton.Query<Province>(sqlCommand, parameters).ToList();
            //Trả về kết quả
            return provinces;
        }
        #endregion
    }
}
