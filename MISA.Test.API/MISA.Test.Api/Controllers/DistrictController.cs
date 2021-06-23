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
    public class DistrictController : MISABaseController<District>
    {
        #region Declare
        IDistrictService _districtService;
        IDistrictRepository _districtRepository;
        #endregion

        #region Constructor
        public DistrictController(IDistrictService districtService,
        IDistrictRepository districtRepository) : base(districtService, districtRepository)
        {
            _districtService = districtService;
            _districtRepository = districtRepository;
        }
        #endregion

        #region Methods
        [HttpGet("ref/{provinceId}")]
        public IActionResult GetByReferenceId([FromRoute] Guid provinceId)
        {
            var districts = _districtRepository.GetByReferenceId(provinceId);

            return Ok(districts);
        }
        #endregion
    }
}
