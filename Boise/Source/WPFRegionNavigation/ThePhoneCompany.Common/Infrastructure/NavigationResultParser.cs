using System;
using Microsoft.Practices.Prism.Regions;

namespace ThePhoneCompany.Common.Infrastructure {
    public class NavigationResultParser {

        private NavigationResultParser() {
        }

        public static String Parse(NavigationResult navigationResult) {
            if(navigationResult.Error == null) {
                return navigationResult.Result.ToString();
            }

            return navigationResult.Error.Message;
        }
    }
}
