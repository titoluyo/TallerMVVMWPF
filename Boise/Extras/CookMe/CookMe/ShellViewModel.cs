using System;
using CookMe.Common.Infrastructure;

namespace CookMe {
    public class ShellViewModel : ObservableObject {

        Boolean _isInKioskMode;

        public Boolean IsInKioskMode {
            get { return _isInKioskMode; }
            set {
                _isInKioskMode = value;
                RaisePropertyChanged("IsInKioskMode");
            }
        }
	
        public ShellViewModel() {
            
        }
    }
}
