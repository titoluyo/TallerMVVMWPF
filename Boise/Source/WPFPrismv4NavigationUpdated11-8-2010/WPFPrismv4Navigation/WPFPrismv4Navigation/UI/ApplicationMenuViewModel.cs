using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using WPFPrismv4Navigation.Constants;
using WPFPrismv4Navigation.Infrastructure;

namespace WPFPrismv4Navigation.UI {

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ApplicationMenuViewModel : ObservableObject, IPartImportsSatisfiedNotification, IConfirmNavigationRequest, INonLinearNavigationObject {
        
        readonly Logger _logger;
        readonly IRegionManager _regionManager;
        String _originalTargetUriString;

        public ICommand NavigateCommand { get { return new DelegateCommand<String>(NavigateExecute); } }
        public ICommand NavigationApiThrowsExceptionCommand { get { return new DelegateCommand(NavigationApiThrowsExceptionExecute); } }
        public ICommand NavigationTargetThrowsExceptionCommand { get { return new DelegateCommand(NavigationTargetThrowsExceptionExecute); } }
        public ICommand NavigationTargetThrowsExceptionAndHandlesCommand { get { return new DelegateCommand(NavigationTargetThrowsExceptionAndHandlesExecute); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationMenuViewModel"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="logger">The logger.</param>
        [ImportingConstructor]
        public ApplicationMenuViewModel(IRegionManager regionManager, Logger logger) {
            _regionManager = regionManager;
            _logger = logger;
            _logger.Log();
        }

        /// <summary>
        /// Navigates to ItemView, passing the argument param.
        /// Notice the use of <see cref="QueryStringBuilder"/> below to construct the query string.  
        /// </summary>
        /// <param name="param">The param.</param>
        void NavigateExecute(String param) {
            _logger.Log(Logger.Scenario.ItemNavigate, Global.NavigationInitiated);
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, QueryStringBuilder.Construct(ViewNames.ItemView, new[,] { { Global.Item, param } }), Callback);
        }

        /// <summary>
        /// When the navigation request is completed, the region manager will call this callback.
        /// This is where the sucess of the operation can be tested and appropriate action taken if the 
        /// navigation request failed.
        /// </summary>
        /// <param name="result">The result.</param>
        void Callback(NavigationResult result) {
            _logger.Log(Global.NavigationRequestResultsCallBack + NavigationResultParser.Parse(result));
        }

        /// <summary>
        /// Attempts to Navigate to an unknown Uri.  The Navigation API returns the exception in the callback.
        /// </summary>
        void NavigationApiThrowsExceptionExecute() {
            _logger.Log(Logger.Scenario.NavigationApiThrowsException, Global.NavigationInitiated);
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, Global.Throw, Callback);
        }

        /// <summary>
        /// Navigates to a view that throws after it has been constructed.  The Navigation API returns the exception in the callback.
        /// However this scenario causes problems if the target does not handle the exception because the navigation pipeline stops.
        /// </summary>
        void NavigationTargetThrowsExceptionExecute() {
            _logger.Log(Logger.Scenario.NavigationTargetThrowsException, Global.NavigationInitiated);
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, QueryStringBuilder.Construct(ViewNames.ItemView, new [,] { { Global.Item, Global.Throw } }), Callback);
        }

        void NavigationTargetThrowsExceptionAndHandlesExecute() {
            _logger.Log(Logger.Scenario.NavigationTargetThrowsExceptionTargetHandles, Global.NavigationInitiated);
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, QueryStringBuilder.Construct(ViewNames.ItemView, new [,] { { Global.Item, Global.ThrowAndHandle } }), Callback);
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
            _logger.Log(Global.DecisionPointUserConfirmsNavigation);
            continuationCallback(true);
        }

        /// <summary>
        /// Called to determine if this instance can handle the navigation request.
        /// This method will need to parse the query string parameters, then match them against a previously stored
        /// value to determine if this object is the actual target of the current navigation request.
        /// 
        /// If there will only ever be one of these objects in the application, the simply return true.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns>
        /// 	<see langword="true"/> if this instance accepts the navigation request; otherwise, <see langword="false"/>.
        /// </returns>
        Boolean INavigationAware.IsNavigationTarget(NavigationContext navigationContext) {
            _logger.Log(Global.DecisionPointAmITheTarget);
            return true;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext) {
            _logger.Log();
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext) {
            if(_originalTargetUriString == null) {
                _originalTargetUriString = navigationContext.Uri.ToString();
            }
            _logger.Log();
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        void IPartImportsSatisfiedNotification.OnImportsSatisfied() {
            _logger.Log();
        }

        /// <summary>
        /// The title is a description of the object.  This title
        /// should easily allow an end user to identity the object
        /// by its title.
        /// For example, for a Customer object you may want to return the First and Last Name.
        /// For an Item object you may want to return the item description.
        /// </summary>
        /// <value></value>
        String INonLinearNavigationObject.Title {
            get { return this.GetType().Name; }
        }

        /// <summary>
        /// The state describes what state the object is in.
        /// For example, adding or editing.
        /// </summary>
        /// <value></value>
        String INonLinearNavigationObject.State {
            get { return Global.Viewing; }
        }

        /// <summary>
        /// The key is the primary key.  This is helpful in scenarios where
        /// the end user is familar with the primary key.
        /// For example, a Customer Number or Item ID.
        /// </summary>
        /// <value></value>
        String INonLinearNavigationObject.Key {
            get { return Global.NoKey; }
        }

        /// <summary>
        /// The Uri that will allow navigating back to this object.
        /// This would include any query string parameters.
        /// This field is typically assigned in the initial OnNavigatedTo method.
        /// </summary>
        /// <value></value>
        String INonLinearNavigationObject.UriString {
            get { return _originalTargetUriString; }
        }

        /// <summary>
        /// The application allows grouping of objects within a region.
        /// For example, if 3 objects are inventory views and 2 objects
        /// are sales objects, you could easily get a count of objects
        /// and display that value in the UI
        /// </summary>
        /// <value></value>
        String INonLinearNavigationObject.Application {
            get { return Global.ApplicationName; }
        }
    }
}
