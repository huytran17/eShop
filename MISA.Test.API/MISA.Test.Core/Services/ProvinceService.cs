using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Services
{
    public class ProvinceService : BaseService<Province>, IProvinceService
    {
        #region Declare
        IProvinceRepository _provinceRepository;
        #endregion

        #region Constructor
        public ProvinceService(IProvinceRepository provinceRepository) : base (provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }
        #endregion
    }
}
