using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using MISA.Core.Interfaces.Services;

namespace MISA.Core.Services
{
    public class StoreService : BaseService<Store>, IStoreService
    {
        #region Declare
        IStoreRepository _shopRepository;
        #endregion

        #region Constructor
        public StoreService(IStoreRepository shopRepository) : base (shopRepository)
        {
            _shopRepository = shopRepository;
        }
        #endregion
    }
}
