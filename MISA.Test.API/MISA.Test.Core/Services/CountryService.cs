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
    public class CountryService : BaseService<Country>, ICountryService
    {
        #region Declare
        ICountryRepository _countryRepository;
        #endregion

        #region Constructor
        public CountryService(ICountryRepository countryRepository) : base (countryRepository)
        {
            _countryRepository = countryRepository;
        }
        #endregion
    }
}
