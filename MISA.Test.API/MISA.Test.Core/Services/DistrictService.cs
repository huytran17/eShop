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
    public class DistrictService : BaseService<District>, IDistrictService
    {
        #region Declare
        IDistrictRepository _districtRepository;
        #endregion

        #region Constructor
        public DistrictService(IDistrictRepository districtRepository) : base (districtRepository)
        {
            _districtRepository = districtRepository;
        }
        #endregion
    }
}
