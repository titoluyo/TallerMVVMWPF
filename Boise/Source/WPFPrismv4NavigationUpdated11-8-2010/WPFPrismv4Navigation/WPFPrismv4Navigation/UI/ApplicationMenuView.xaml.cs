using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using WPFPrismv4Navigation.Constants;
using WPFPrismv4Navigation.Infrastructure;

namespace WPFPrismv4Navigation.UI {

    /// <summary>
    /// The Export name is the "key" that the navigation system will use when calling into the 
    /// container to create this object.  This same "key" must be used when building up the
    /// Uri object or Uri string that is passed to the RequestNavigate method.
    /// 
    /// Notice that this View has a CreationPolicy of Shared.  This instructs MEF to provide
    /// a the same instance from the container as opposed to returning a new instance.
    /// 
    /// Since this view acts as the application menu page, there is only one instance.
    /// </summary>
    [Export(ViewNames.ApplicationMenuView)]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ApplicationMenuView : UserControl, IPartImportsSatisfiedNotification, IConfirmNavigationRequest {

        [Import]
        public IRegionManager RegionManager { get; set; }

        [Import]
        Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ApplicationMenuViewModel"/>.
        /// This is the recommended technique for wiring up a View's ViewModel.
        /// This technique is Designer friendly and works when using MEF or an IOC container.
        /// 
        /// In a typical WPF or Silverlight ViewModel style application, this is usually the only code
        /// in addition to the constructor in the code-behind.
        /// 
        /// When using this technique, provide a good design-time experience in the XAML
        /// by using either d:DataContext, d:DesignSource assigning d:DesignInstance or d:DesignData
        /// </summary>
        [Import]
        ApplicationMenuViewModel ApplicationMenuViewModel {
            get { return this.DataContext as ApplicationMenuViewModel; }
            set { this.DataContext = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationMenuView"/> class.
        /// </summary>
        public ApplicationMenuView() {
            InitializeComponent();
        }

        /// <summary>
        /// Determines whether this instance accepts being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <param name="continuationCallback">The callback to indicate when navigation can proceed.</param>
        /// <remarks>
        /// Implementors of this method do not need to invoke the callback before this method is completed,
        /// but they must ensure the callback is eventually invoked.
        /// </remarks>
        void IConfirmNavigationRequest.ConfirmNavigationRequest(NavigationContext navigationContext, Action<Boolean> continuationCallback) {
            this.Logger.Log(Global.DecisionPointUserConfirmsNavigation);
            continuationCallback(true);
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>
        /// 	<see langword="true"/> if this instance accepts the navigation request; otherwise, <see langword="false"/>.
        /// </returns>
        Boolean INavigationAware.IsNavigationTarget(NavigationContext navigationContext) {
            this.Logger.Log(Global.DecisionPointAmITheTarget);
            return true;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext) {
            this.Logger.Log();
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext) {
            this.Logger.Log();
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        void IPartImportsSatisfiedNotification.OnImportsSatisfied() {
            this.Logger.Log();
        }
    }
}
