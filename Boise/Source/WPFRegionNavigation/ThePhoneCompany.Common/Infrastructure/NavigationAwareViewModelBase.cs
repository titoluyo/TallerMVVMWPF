using System;
using Microsoft.Practices.Prism.Regions;

namespace ThePhoneCompany.Common.Infrastructure {
    public class NavigationAwareViewModelBase : ViewModelBase, IConfirmNavigationRequest {
        
        #region Declarations

        readonly IRegionManager _regionManager;
        String _errorMessage;
        
        #endregion //Declarations
        
        #region Properties

        public String ErrorMessage {
            get { return _errorMessage; }
            set {
                _errorMessage = value;
                this.RaisePropertyChanged("ErrorMessage");
            }
        }

        protected IRegionManager RegionManager {
            get { return _regionManager; }
        }
        
        #endregion //Properties

        #region Constructor

        public NavigationAwareViewModelBase(IRegionManager regionManager) {
            _regionManager = regionManager;
        }

        #endregion //Constructor

        #region Methods

        protected void DisplayException(Exception exception) {
#if DEBUG
            this.ErrorMessage = exception.ToString();
#else
            this.ErrorMessage = exception.Message;
#endif
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<Boolean> continuationCallback) {
            continuationCallback(true);
        }

        public virtual Boolean IsNavigationTarget(NavigationContext navigationContext) {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }

        public virtual void OnNavigatedTo(NavigationContext navigationContext) { }

        protected void NavigateRequest(String regionName, String target) {
            this.RegionManager.Regions[regionName].RequestNavigate(target);
        }

        protected void NavigateRequest(String regionName, String target, Action<NavigationResult> navigationCallback) {
            this.RegionManager.Regions[regionName].RequestNavigate(target, navigationCallback);
        }

        protected void NavigateRequest(String regionName, String target, String[,] parms) {
            this.RegionManager.Regions[regionName].RequestNavigate(new Uri(QueryStringBuilder.Construct(target, parms), UriKind.Relative));
        }

        protected void NavigateRequest(String regionName, String target, Action<NavigationResult> navigationCallback, String[,] parms) {
            this.RegionManager.Regions[regionName].RequestNavigate(new Uri(QueryStringBuilder.Construct(target, parms), UriKind.Relative), navigationCallback);
        }

        protected void NavigateRequest(String regionName, String target, String paramName, String paramValue) {
            this.NavigateRequest(regionName, target, new[,] { { paramName, paramValue } });
        }

        protected void NavigateRequest(String regionName, String target, Action<NavigationResult> navigationCallback, String paramName, String paramValue) {
            this.NavigateRequest(regionName, target, navigationCallback, new[,] { { paramName, paramValue } });
        }

        #endregion //Methods
    }
}
