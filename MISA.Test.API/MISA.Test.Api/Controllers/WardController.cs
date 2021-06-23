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
    public class WardController : MISABaseController<Ward>
    {
        #region Declare
        IWardService _wardService;
        IWardRepository _wardRepository;
        #endregion

        #region Constructor
        public WardController(IWardService wardService,
        IWardRepository wardRepository) : base(wardService, wardRepository)
        {
            _wardService = wardService;
            _wardRepository = wardRepository;
        }
        #endregion

        #region Methods
        [HttpGet("ref/{districtId}")]
        public IActionResult GetByReferenceId([FromRoute] Guid districtId)
        {
            var wards = _wardRepository.GetByReferenceId(districtId);

            return Ok(wards);
        }
        #endregion
    }
}
