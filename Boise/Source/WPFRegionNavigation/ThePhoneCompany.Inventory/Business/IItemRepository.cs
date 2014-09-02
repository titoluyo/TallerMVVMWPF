using System;
using System.Collections.Generic;
using ThePhoneCompany.Inventory.ThePhoneCompanyDataService;

namespace ThePhoneCompany.Inventory.Business {
    public interface IItemRepository  {

        void GetAll(Action<IEnumerable<Item>> resultCallback, Action<Exception> errorCallback);

        void Get(Int32 itemId, Action<Item> resultCallback, Action<Exception> errorCallback);

        Item Create();
    }
}
