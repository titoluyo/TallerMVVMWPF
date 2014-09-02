using System;

namespace Wpf.Common.Unity {
    /// <summary>
    /// Consumed during bootstrapping.  Indicates the view model the view wants created and set as its DataContext.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ViewContainerInitializerAttribute : Attribute {

        /// <summary>
        /// Gets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public Type ViewModel { get; private set; }

        /// <summary>
        /// Gets or sets the register for region navigation.
        /// </summary>
        /// <value>The register for region navigation.</value>
        public RegisterForRegionNavigation RegisterForRegionNavigation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewContainerInitializerAttribute"/> class without a view model.
        /// </summary>
        /// <param name="registerForRegionNavigation">Register for region navigation; when Yes registers the view with the container using Object to view type mapping; when No, registers the view type with the container.</param>
        public ViewContainerInitializerAttribute(RegisterForRegionNavigation registerForRegionNavigation = RegisterForRegionNavigation.Yes) {
            this.ViewModel = null;
            this.RegisterForRegionNavigation = registerForRegionNavigation;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewContainerInitializerAttribute"/> class.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <param name="registerForRegionNavigation">Register for region navigation; when Yes registers the view with the container using Object to view type mapping; when No, registers the view type with the container.</param>
        public ViewContainerInitializerAttribute(Type viewModel, RegisterForRegionNavigation registerForRegionNavigation = RegisterForRegionNavigation.Yes) {
            this.ViewModel = viewModel;
            this.RegisterForRegionNavigation = registerForRegionNavigation;
        }
    }
}
