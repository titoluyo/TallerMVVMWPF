using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Wpf.Common.Infrastructure;

//=====================================================================================================
// THIS IS DEMO CODE ONLY - PLEASE DO NOT WRITE YOUR LINE OF BUSSINESS APPLICATIONS LIKE THIS
//=====================================================================================================
namespace Wpf.Validation.Model {

    public class Contact : ObservableObject, IDataErrorInfo {

        String _firstName;
        String _lastName;
        Int32 _numberOfComputers;
        DateTime? _birthday;
        readonly Dictionary<String, String> _errors;

        public String FirstName {
            get { return _firstName; }
            set {
                _firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public String LastName {
            get { return _lastName; }
            set {
                _lastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public Int32 NumberOfComputers {
            get { return _numberOfComputers; }
            set {
                _numberOfComputers = value;
                RaisePropertyChanged("NumberOfComputers");
            }
        }
        
        public DateTime? Birthday {
            get { return _birthday; }
            set {
                _birthday = value;
                RaisePropertyChanged("Birthday");
            }
        }

        public String Error {
            get {
                var sb = new StringBuilder(256);
                foreach (var error in this.Errors) {
                    sb.Append(error.Value).Append(", ");
                }
                
                //this removes the last comma and space
                if (sb.Length > 2) {
                    sb.Length = sb.Length - 2;
                }

                return sb.ToString();
            }
        }
        
        public IDictionary<String, String> Errors {
            get { return _errors; }
        }

        //This property clearly demonstrates why a validation framework needed for line of business applications
        //
        // 1. very fragile code
        // 2. is not DRY when compared to a validation frameworks implementation
        //
        public String this[String propertyName] {
            get {
                String errorMessage = String.Empty;
                this.Errors.Remove(propertyName);

                switch (propertyName) {
                    case "Birthday":
                        if (this.Birthday.HasValue && this.Birthday > DateTime.Today) {
                            errorMessage = "Birthday must be either be today or before today.";
                        }
                        break;

                    case "NumberOfComputers":
                        if (this.NumberOfComputers < 0) {
                            errorMessage = "Number of computers must be greater than or equal to zero.";
                        }
                        break;

                    case "FirstName":
                        if (String.IsNullOrWhiteSpace(this.FirstName)) {
                            errorMessage = "First name is a required field.";
                        }
                        break;

                    case "LastName":
                        if (string.IsNullOrWhiteSpace(this.LastName)) {
                            errorMessage = "Last name is a required field.";
                        }
                        break;

                }

                if (!string.IsNullOrEmpty(errorMessage)) {
                    this.Errors.Add(propertyName, errorMessage);
                }

                RaisePropertyChanged("Error");
                return errorMessage;
            }
        }

        public Contact() {
            _errors = new Dictionary<String, String>();
        }
    }
}
//=====================================================================================================
// THIS IS DEMO CODE ONLY - PLEASE DO NOT WRITE YOUR LINE OF BUSSINESS APPLICATIONS LIKE THIS
//=====================================================================================================
