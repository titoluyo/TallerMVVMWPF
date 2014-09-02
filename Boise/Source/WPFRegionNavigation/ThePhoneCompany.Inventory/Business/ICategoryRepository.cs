using System;
using System.Collections.Generic;
using ThePhoneCompany.Inventory.ThePhoneCompanyDataService;

namespace ThePhoneCompany.Inventory.Business {
    public interface ICategoryRepository {

        void GetAll(Action<IEnumerable<Category>> resultCallback, Action<Exception> errorCallback);

        void Get(Int32 categoryId, Action<Category> resultCallback, Action<Exception> errorCallback);

        Category Create();
    }
}
