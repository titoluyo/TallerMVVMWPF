using System;
using Microsoft.Practices.Prism.Regions;

namespace ThePhoneCompany.Common.Infrastructure {
    public abstract class FormNavigationAwareViewModelBase : NavigationAwareViewModelBase, IRegionMemberLifetime, INonLinearNavigationObject {
        
        #region Declarations

        Boolean _keepAlive = true;
        String _title = Constants.Constants.NoSet;
        String _state = Constants.Constants.NoSet;
        String _key = Constants.Constants.NoSet;
        String _uriString = Constants.Constants.NoSet;

        #endregion //Declarations
        
        #region Constructor

        protected FormNavigationAwareViewModelBase(IRegionManager regionManager)
            : base(regionManager) {
        }

        #endregion //Constructor

        #region Methods

        protected void SetKeepAliveFalse() {
            _keepAlive = false;
        }

        protected void SetNonLinearNavigationObject(String title, String state, String key, String application) {
            _title = title;
            _state = state;
            _key = key;
        }

        protected virtual void OnNavigatedTo(String key) { }
        protected virtual void OnNavigatedTo(Int32 key) { }

        protected virtual Boolean IsNavigationTarget(String key) { return true; }
        protected virtual Boolean IsNavigationTarget(Int32 key) { return true; }

        public override Boolean IsNavigationTarget(NavigationContext navigationContext) {

            String key = navigationContext.Parameters[Constants.Constants.Key];

            if(!String.IsNullOrWhiteSpace(key)) {
                Int32 keyId;
                return Int32.TryParse(key, out keyId) ? this.IsNavigationTarget(keyId) : this.IsNavigationTarget(key);
            }
            throw new InvalidOperationException(Constants.Constants.KeyNotSet);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
            if(_key == null || _key == Constants.Constants.NoSet) {
                _uriString = navigationContext.Uri.ToString();

                String key = navigationContext.Parameters[Constants.Constants.Key];

                if(!String.IsNullOrWhiteSpace(key)) {
                    Int32 keyId;
                    if(Int32.TryParse(key, out keyId)) {
                        this.OnNavigatedTo(keyId);
                    } else {
                        this.OnNavigatedTo(key);
                    }
                } else {
                    throw new InvalidOperationException(Constants.Constants.KeyNotSet);
                }
            }
        }

        public String Title {
            get { return _title; }
        }

        public String State {
            get { return _state; }
        }

        public String Key {
            get { return _key; }
        }

        public String UriString {
            get { return _uriString; }
        }

        public abstract String Application { get; }

        public Boolean KeepAlive {
            get { return _keepAlive; }
        }

        #endregion //Methods
    }
}
