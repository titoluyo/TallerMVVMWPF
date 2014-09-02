using System;
using System.Windows;
using System.Windows.Input;
using Wpf.Common.Infrastructure;
using Wpf.Validation.Infrastructure;
using Wpf.Validation.Model;

namespace Wpf.Validation.Rules {

    public class ContactViewModel : MaintenanceFormViewModelBase {

        #region Declarations

        Contact _contact;
        ICommand _newCommand;
        ICommand _saveCommand;

        #endregion

        #region Properties

        public Contact Contact {
            get { return _contact; }
            set {
                _contact = value;
                RaisePropertyChanged("Contact");
            }
        }

        #endregion

        #region Command Properties

        public ICommand NewCommand {
            get { return _newCommand ?? (_newCommand = new RelayCommand(NewExecute)); }
        }

        public ICommand SaveCommand {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(SaveExecute, CanSaveExecute)); }
        }

        #endregion

        #region Constructor

        public ContactViewModel() {
            _contact = new Contact { Birthday = new DateTime(1990, 11, 1), FirstName = "Aaberg", LastName = "Jesper", NumberOfComputers = 2 };
        }

        #endregion

        #region Command Methods

        void NewExecute() {
            //TODO: Technically you should save old one,etc  
            this.Contact = new Contact();
        }

        void SaveExecute() {
            //In a real applicaiton implement saving the Contact to the database
            //In M-V-VM applications the ViewModel does not raise UI Message Boxes.
            //  Instead, a Logger or Dialog service is used.
            MessageBox.Show("Saved");
        }

        Boolean CanSaveExecute() {
            if (this.Contact == null) return false;
            return this.Contact.Errors.Count == 0;
            //return this.ViewValidationErrorCount == 0 && this.Contact.Errors.Count == 0;
        }
        #endregion
    }
}
