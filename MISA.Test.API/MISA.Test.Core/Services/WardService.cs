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
    public class WardService : BaseService<Ward>, IWardService
    {
        #region Declare
        IWardRepository _wardRepository;
        #endregion

        #region Constructor
        public WardService(IWardRepository wardRepository) : base (wardRepository)
        {
            _wardRepository = wardRepository;
        }
        #endregion
    }
}
