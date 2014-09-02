using System;
using Microsoft.Practices.Prism.Regions;

namespace WPFPrismv4Navigation.Infrastructure {
    public class NavigationResultParser {

        private NavigationResultParser() {
        }

        public static String Parse(NavigationResult navigationResult) {
            return navigationResult.Error == null ? navigationResult.Result.ToString() : navigationResult.Error.Message;
        }
    }
}
