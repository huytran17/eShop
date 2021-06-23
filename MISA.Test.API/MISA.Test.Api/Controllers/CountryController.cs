using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Test.Api.Controllers
{
    [Route("api/v1/Countries")]
    [ApiController]
    public class CountryController : MISABaseController<Country>
    {
        #region Declare
        ICountryService _countryService;
        ICountryRepository _countryRepository;
        #endregion

        #region Constructor
        public CountryController(ICountryService countryService,
        ICountryRepository countryRepository) : base(countryService, countryRepository)
        {
            _countryService = countryService;
            _countryRepository = countryRepository;
        }
        #endregion
    }
}
