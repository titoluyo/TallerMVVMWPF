using System;

namespace Wpf.Common.Unity {
    /// <summary>
    /// Consumed during bootstrapping.  Registers a view model in the container.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ViewModelContainerInitializerAttribute : Attribute {

        /// <summary>
        /// Gets or sets the register for region navigation.
        /// </summary>
        /// <value>The register for region navigation.</value>
        public RegisterForRegionNavigation RegisterForRegionNavigation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelContainerInitializerAttribute"/> class without a view model.
        /// </summary>
        /// <param name="registerForRegionNavigation">Register for region navigation; when Yes registers the view with the container using Object to view type mapping; when No, registers the view type with the container.</param>
        public ViewModelContainerInitializerAttribute(RegisterForRegionNavigation registerForRegionNavigation = RegisterForRegionNavigation.Yes) {
            this.RegisterForRegionNavigation = registerForRegionNavigation;
        }
    }
}
