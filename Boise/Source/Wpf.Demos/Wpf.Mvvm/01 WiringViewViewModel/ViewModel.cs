using System;
using Wpf.Common.Infrastructure;
using Wpf.Mvvm.Model;
using System.Windows.Input;

namespace Wpf.Mvvm.WiringViewViewModel {

    public class ViewModel : ObservableObject {

        Person _person = new Person();
        String _message;

        public Person Person {
            get { return _person; }
            set {
                _person = value;
                RaisePropertyChanged("Person");
            }
        }
        
        public String Message {
            get { return _message; }
            private set {
                _message = value;
                RaisePropertyChanged("Message");
            }
        }

        ICommand _saveCommand;
        
        public ICommand SaveCommand {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand<Person>(SaveExecute)); }
        }
	

        public ViewModel() {

        }

        void SaveExecute(Person person) {
            this.Message = String.Format("{0} {1} saved to database.", person.FirstName, person.LastName);
        }
    }
}