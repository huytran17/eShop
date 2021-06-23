using Microsoft.AspNetCore.Mvc;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;
using System;
using System.Collections.Generic;

namespace MISA.Test.Api.Controllers
{
    public class StoreController : MISABaseController<Store>
    {
        #region Declare
        IStoreService _storeService;
        IStoreRepository _storeRepository;
        #endregion

        #region Constructor
        public StoreController(IStoreService storeService,
        IStoreRepository storeRepository) : base (storeService, storeRepository)
        {
            _storeService = storeService;
            _storeRepository = storeRepository;
        }
        #endregion

        [HttpGet("exists/{storeCode}")]
        public IActionResult CheckExistsStoreCode([FromRoute] String storeCode)
        {
            //Kiểm tra mã cửa hàng có tồn tại hay không
            var result = _storeRepository.IsExistsStoreCode(storeCode);
            //Trả về kết quả
            return Ok(result);
        }

        [HttpGet("Paging")]
        public IActionResult GetPaging(int? pageSize = 50, int? pageIndex = 1)
        {
            //Lấy danh sách cửa hàng theo phân trang
            var stores = _storeRepository.GetPaging(pageSize, pageIndex);
            //Tạo đối tượng chứa thông tin phân trang
            var paging = new Paging(stores.totalRecords, stores.totalPages, stores.listStores);
            //Trả về kết quả
            return Ok(paging);
        }
#nullable enable
        [HttpGet("PagingFilter")]
        public IActionResult GetPagingFilter(int? pageSize = 50, int? pageIndex = 1, String? storeCode = "", String? storeName = "", String? address = "", String? phoneNumber = "", int? status = 2)
        {
            var stores = _storeRepository.GetPagingFilter(pageSize, pageIndex, storeCode, storeName, address, phoneNumber, status);

            //Tạo đối tượng chứa thông tin phân trang
            var paging = new Paging(stores.totalRecords, stores.totalPages, stores.listStores);

            return Ok(paging);
        }
    }

    public class Paging
    {
        #region Declare
        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Danh sách cửa hàng theo phân trang
        /// </summary>
        public List<Store> ListStores { get; set; } = new List<Store>();
        #endregion

        #region Constructor
        public Paging(int totalRecords, int totalPages, List<Store> listStores)
        {
            this.TotalRecords = totalRecords;
            this.TotalPages = totalPages;
            this.ListStores = listStores;
        }
        #endregion
    }
}
