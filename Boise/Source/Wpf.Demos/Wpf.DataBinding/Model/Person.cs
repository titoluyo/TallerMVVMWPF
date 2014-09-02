using System;
using Wpf.Common.Infrastructure;

namespace Wpf.DataBinding.Model {

    public class Person : ObservableObject {

        String _firstName;
        String _lastName;
        DateTime? _birthday;
        String _profession;
        Boolean _isActive;
        String _favoriteChairThumbnail;

        public String FavoriteChairThumbnail {
            get { return _favoriteChairThumbnail; }
            set {
                _favoriteChairThumbnail = value;
                RaisePropertyChanged("FavoriteChairThumbnail");
            }
        }
	
        public Boolean IsActive {
            get { return _isActive; }
            set {
                _isActive = value;
                RaisePropertyChanged("IsActive");
            }
        }

        public String Profession {
            get { return _profession; }
            set {
                _profession = value;
                RaisePropertyChanged("Profession");
            }
        }

        public DateTime? Birthday {
            get { return _birthday; }
            set {
                _birthday = value;
                RaisePropertyChanged("Birthday");
            }
        }

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

        public Person() {

        }

        //Note:  ToString is NOT dynamically updated in the UI like an INotifyPropertyChanged property
        public override string ToString() {
            return (String.Concat(_firstName, " ", _lastName)).Trim();
        }
    }
}
