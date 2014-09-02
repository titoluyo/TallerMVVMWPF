using System.Data.Services;
using System.Data.Services.Common;

namespace ThePhoneCompany.DataService {
    public class ThePhoneCompanyService : DataService<ThePhoneCompany.DataService.ThePhoneCompanyEntities> {
        // This method is called only once to initialize service-wide policies.
        public static void InitializeService(DataServiceConfiguration config) {
            // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
            // Examples:
            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
        }
    }
}
