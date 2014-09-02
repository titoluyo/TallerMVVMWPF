using System;

namespace WPFPrismv4Navigation.Constants {

    /// <summary>
    /// Having a standard for putting all view name constants in a separate class 
    /// makes reading the code easier over time as your application grows in size and scope.
    /// These constants are used in the Export attributes that decordate each of the 
    /// view exports when using MEF as your application container.
    /// These constants are also used when creating Uri's to navigate to that View.
    /// </summary>
    public class ViewNames {

        public const String ApplicationMenuView = "ApplicationMenuView";
        public const String ItemView = "ItemView";

    }
}
