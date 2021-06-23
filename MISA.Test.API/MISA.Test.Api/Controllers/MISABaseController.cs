using Microsoft.AspNetCore.Mvc;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Test.Api.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class MISABaseController<MISAEntity> : ControllerBase
    {
        #region Declare
        IBaseService<MISAEntity> _baseService;
        IBaseRepository<MISAEntity> _baseRepository;
        #endregion

        #region Constructor
        public MISABaseController(IBaseService<MISAEntity> baseService,
        IBaseRepository<MISAEntity> baseRepository)
        {
            _baseService = baseService;
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        // GET: api/<MISABaseController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _baseRepository.GetAll();
            //Nếu có ít nhất 1 bản ghi
            if (entities.Count > 0)
                //Trả về kết quả
                return Ok(entities);
            else
                return NoContent();
        }

        // GET api/<MISABaseController>/5
        [HttpGet("{entityId}")]
        public IActionResult GetById([FromRoute] Guid entityId)
        {
            //Lấy theo id
            var entity = _baseRepository.GetById(entityId);
            return Ok(entity);
        }

        // POST api/<MISABaseController>
        [HttpPost]
        public IActionResult Post([FromBody] MISAEntity entity)
        {
            //Thêm mới bản ghi
            var response = _baseService.Insert(entity);
            return Ok(response);
        }

        // PUT api/<MISABaseController>/5
        [HttpPut("{entityId}")]
        public IActionResult Put([FromBody] MISAEntity entity, [FromRoute] Guid entityId)
        {
            //Chỉnh sửa bản ghi
            var response = _baseService.Update(entity, entityId);
            return Ok(response);
        }

        // DELETE api/<MISABaseController>/5
        [HttpDelete("{entityId}")]
        public IActionResult Delete([FromRoute] Guid entityId)
        {
            //Xóa bản ghi 
            var response = _baseService.Delete(entityId);
            return Ok(response);
        }
        #endregion
    }
}
