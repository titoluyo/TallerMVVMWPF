using System;
using Microsoft.Practices.Prism.Regions;
using Wpf.Common.Infrastructure;
using Wpf.Navigation.Services;

namespace Wpf.Navigation.Navigation {

    public class DataFormViewModel : ObservableObject, IConfirmNavigationRequest, IRegionMemberLifetime {

        readonly IDialogService _dialogService;
        String _queryString;
        Boolean _confirmNavigation;
        Boolean _keepMeAlive;
        String _id;
        String _timeLoaded;
        
        public String TimeLoaded {
            get { return _timeLoaded; }
            set {
                _timeLoaded = value;
                RaisePropertyChanged("TimeLoaded");
            }
        }
	
        public String Id {
            get { return _id; }
            private set {
                _id = value;
                RaisePropertyChanged("Id");
            }
        }
	
        public Boolean KeepMeAlive {
            get { return _keepMeAlive; }
            set {
                _keepMeAlive = value;
                RaisePropertyChanged("KeepMeAlive");
            }
        }

        public Boolean ConfirmNavigation {
            get { return _confirmNavigation; }
            set {
                _confirmNavigation = value;
                RaisePropertyChanged("ConfirmNavigation");
            }
        }

        public String QueryString {
            get { return _queryString; }
            set {
                _queryString = value;
                RaisePropertyChanged("QueryString");
            }
        }

        public DataFormViewModel(IDialogService dialogService) {
            _dialogService = dialogService;
            if(dialogService == null) throw new ArgumentNullException("dialogService");
            this.TimeLoaded = DateTime.Now.ToLongTimeString();
        }

        public void OnNavigatedTo(NavigationContext navigationContext) {
            this.QueryString = navigationContext.Uri.ToString();

            this.Id = navigationContext.Parameters["ID"];
        }

        public Boolean IsNavigationTarget(NavigationContext navigationContext) {
            return this.Id == navigationContext.Parameters["ID"];
        }

        public void OnNavigatedFrom(NavigationContext navigationContext) {

        }

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<Boolean> continuationCallback) {
            Boolean allowNavigation = true;
            if(_confirmNavigation) {
                allowNavigation = _dialogService.ShowMessage("OK to navigate away", "", DialogButton.OKCancel, DialogImage.Question) == DialogResponse.OK;
            }
            continuationCallback(allowNavigation);
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return _keepMeAlive; }
        }
    }
}
