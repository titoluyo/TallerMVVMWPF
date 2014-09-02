using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using WPFPrismv4Navigation.Infrastructure;

namespace WPFPrismv4Navigation.UI {

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ItemViewModel : ObservableObject, IPartImportsSatisfiedNotification, IConfirmNavigationRequest, IRegionMemberLifetime, INonLinearNavigationObject {

        readonly Logger _logger;
        IRegionManager _regionManager;
        IRegionNavigationJournal _navigationJournal;
        Boolean _keepAliveState = true;
        Boolean _cancelNavigation;
        String _currentItem;
        String _originalTargetUriString;

        /// <summary>
        /// Gets or sets the current item.  This is set by the below <see cref="INavigationAware.OnNavigatedTo"/> method.
        /// This method parses the query string parameters and assigns this value.
        /// </summary>
        /// <value>The current item.</value>
        public String CurrentItem {
            get { return _currentItem; }
            set {
                _currentItem = value;
                this.RaisePropertyChanged(() => CurrentItem);
            }
        }

        /// <summary>
        /// Property allows testing of the cancel navigation feature of Prism
        /// </summary>
        public Boolean CancelNavigation {
            get { return _cancelNavigation; }
            set {
                _cancelNavigation = value;
                this.RaisePropertyChanged(() => CancelNavigation);
            }
        }

        public ICommand CloseCommand { get { return new DelegateCommand(CloseExecute); } }
        public ICommand JournalBackCommand { get { return new DelegateCommand(JournalBackExecute); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemViewModel"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="logger">The logger.</param>
        [ImportingConstructor]
        public ItemViewModel(IRegionManager regionManager, Logger logger) {
            _regionManager = regionManager;
            _logger = logger;
            _logger.Log();
            this.CancelNavigation = false;
        }

        /// <summary>
        /// Closes the view by:
        /// 1. Assigning the <see cref="_keepAliveState"/> field to false.  
        ///    This field's value is returned when the <see cref="IRegionMemberLifetime.KeepAlive"/> property is
        ///    access during navigation.  If false, the navigation system will remove this object from the region.
        ///    The navigation system is smart.  It checks both the View and View's ViewModel to see if they implement
        ///    this interface.  So, even if the ViewModel is the only object to provide implementation, the View/ViewModel
        ///    pair will still be removed from the region.
        ///   
        /// 2. Using the journal to navigate back.
        /// </summary>
        void CloseExecute() {
            if(_navigationJournal.CanGoBack) {
                _keepAliveState = false;
                _logger.Log(Logger.Scenario.CloseView, Constants.Global.CloseJournalBackInitiated);
                _navigationJournal.GoBack();
            }
        }

        /// <summary>
        /// Use the journal to navigate back.
        /// </summary>
        void JournalBackExecute() {
            if(_navigationJournal.CanGoBack) {
                _logger.Log(Logger.Scenario.GoBack, Constants.Global.JournalBackInitiated);
                _navigationJournal.GoBack();
            }
        }

        /// <summary>
        /// When the navigation request is completed, the region manager will call this callback.
        /// This is where the sucess of the operation can be tested and appropriate action taken if the 
        /// navigation request failed.
        /// </summary>
        /// <param name="result">The result.</param>
        void Callback(NavigationResult result) {
            _logger.Log(Constants.Global.NavigationRequestResultsCallBack + NavigationResultParser.Parse(result));
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
            _logger.Log(Constants.Global.DecisionPointUserConfirmsNavigation);
            //continuationCallback(true);
            continuationCallback(!this.CancelNavigation);
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

            String item = null;

            if(navigationContext.Parameters != null) {
                item = navigationContext.Parameters[Constants.Global.Item];
            }

            Boolean result = this.CurrentItem == item;
            _logger.Log(Constants.Global.DecisionPointAmITheTarget + result);
            return result;
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <remarks>This method can be used to save state.  For example, save the currently focused
        /// control and then when navigated back to, restore focus to that control.</remarks>
        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext) {
            _logger.Log();
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext) {
            _logger.Log();
            if(_navigationJournal == null) {
                _navigationJournal = navigationContext.NavigationService.Journal;
            }
            if(_originalTargetUriString == null) {
                _originalTargetUriString = navigationContext.Uri.ToString();
            }

            //This is the scenario you want to avoid.
            //This View/ViewModel has already been constructed and this form should be handling its
            //own exceptions.  
            //
            //Do not allow local exceptions to bubble up to the navigation API after the object is constructed and visible.
            //
            //The reason for not allowing this is, no further navigation pipeline processing will occur and you 
            //may find your application in an unexpected state, example:  view dispalyed without any data.
            //
            if(navigationContext.Parameters != null && navigationContext.Parameters[Constants.Global.Item] == Constants.Global.Throw) {
                throw new Exception(Constants.Global.ExceptionThrownAfterNavigationCompleted);
            }

            //This simulates calling into a repository to get the required data.
            //While getting the data the repository throws.  
            //In this example we will handle the exception correctly rather than
            //allow a local exception to be bubbled up to the navigation API.
            //
            //Takaway:  Non navigation related exceptions should be handled by the View/ViewModel and not the navigation API.
            if(navigationContext.Parameters != null && navigationContext.Parameters[Constants.Global.Item] == Constants.Global.ThrowAndHandle) {
                this.LoadDataFromRepositoryHandleExceptionCorrectly();
            }

            //only set the current item on the initial navigation.
            if(navigationContext.Parameters != null && this.CurrentItem == null) {
                this.CurrentItem = navigationContext.Parameters[Constants.Global.Item];
            }
        }

        void LoadDataFromRepositoryHandleExceptionCorrectly() {
            //simulate a call to the repository
            try {
                throw new Exception(Constants.Global.RepositoryExceptionMessage);
            } catch(Exception ex) {
                _logger.Log(String.Format(Constants.Global.ExceptionCaughtLogMessage_FormatString, ex.Message));

                //PLEASE NEVER call MessageBox from a ViewModel.  
                //This was done here to keep the code simple and about
                //navigation and not View/ViewModel UI interactiions.
                MessageBox.Show(Constants.Global.HandledExceptionMessage, Constants.Global.Exception, MessageBoxButton.OK, MessageBoxImage.Error);

                //NOTE: keep alive to false so that this View/ViewModel will be removed
                _keepAliveState = false;

                _logger.Log(Logger.Scenario.GoBackToOriginalCaller, Constants.Global.JournalBackInitiatedAfterException);
                _navigationJournal.GoBack();
            }
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        void IPartImportsSatisfiedNotification.OnImportsSatisfied() {
            _logger.Log();
        }

        /// <summary>
        /// Gets a value indicating whether this instance should be kept-alive upon deactivation.
        /// 
        /// If <c>false</c>, the navigation system will remove this object from the region.
        /// The navigation system is smart.  It checks both the View and View's ViewModel to see if they implement
        /// this interface.  So, even if the ViewModel is the only object to provide implementation, the View/ViewModel
        /// pair will still be removed from the region.
        /// </summary>
        Boolean IRegionMemberLifetime.KeepAlive {
            get {
                _logger.Log(Constants.Global.DecisionPointKeepAliveValueReturned + _keepAliveState);
                return _keepAliveState;
            }
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
            get { return Constants.Global.Viewing; }
        }

        /// <summary>
        /// The key is the primary key.  This is helpful in scenarios where
        /// the end user is familar with the primary key.
        /// For example, a Customer Number or Item ID.
        /// </summary>
        /// <value></value>
        String INonLinearNavigationObject.Key {
            get { return this.CurrentItem; }
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
            get { return Constants.Global.ApplicationName; }
        }
    }
}
