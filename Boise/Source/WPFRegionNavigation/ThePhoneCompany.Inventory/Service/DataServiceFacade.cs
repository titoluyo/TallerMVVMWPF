using System;
using System.ComponentModel.Composition;
using ThePhoneCompany.Common.Constants;
using ThePhoneCompany.Inventory.ThePhoneCompanyDataService;

namespace ThePhoneCompany.Inventory.Service {

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DataServiceFacade {
        
        readonly ThePhoneCompanyEntities _entities;

        public ThePhoneCompanyEntities DataService {
            get { return _entities; }
        }

        public DataServiceFacade() {
            _entities = new ThePhoneCompanyEntities(new Uri(Constants.ThePhoneCompanyUri));
        }
    }
}
