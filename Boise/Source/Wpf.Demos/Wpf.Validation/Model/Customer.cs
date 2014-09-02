using System;
using Wpf.Common.Infrastructure;

namespace Wpf.Validation.Model {

    public class Customer : ObservableObject {

        String _firstName;
        String _lastName;

        public String LastName {
            get { return _lastName; }
            set {
                _lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public String FirstName {
            get { return _firstName; }
            set {
                _firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public Customer() {

        }
    }
}
