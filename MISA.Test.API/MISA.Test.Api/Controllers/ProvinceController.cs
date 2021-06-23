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
    public class ProvinceController : MISABaseController<Province>
    {
        #region Declare
        IProvinceService _provinceService;
        IProvinceRepository _provinceRepository;
        #endregion

        #region Constructor
        public ProvinceController(IProvinceService provinceService,
        IProvinceRepository provinceRepository) : base(provinceService, provinceRepository)
        {
            _provinceService = provinceService;
            _provinceRepository = provinceRepository;
        }
        #endregion

        #region Methods
        [HttpGet("ref/{countryId}")]
        public IActionResult GetByReferenceId([FromRoute] Guid countryId)
        {
            var provinces = _provinceRepository.GetByReferenceId(countryId);

            return Ok(provinces);
        }
        #endregion
    }
}
