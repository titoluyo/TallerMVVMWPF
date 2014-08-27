using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WpfModelValidation 
{
    class Person : IDataErrorInfo, INotifyPropertyChanged
    {
        private string _PersonName;
        private int _Age;
        public string PersonName
        {
            get
            {
                return _PersonName;
            }
            set
            {
                _PersonName = value;
                NotifyPropertyChanged("PersonName");
            }
        }
        public int Age
        {
            get
            {
                return _Age;
            }
            set
            {
                _Age = value;
                NotifyPropertyChanged("Age");
            }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                if (propertyName == "PersonName")
                {
                    if (PersonName.Length > 30 || PersonName.Length < 1)
                    {
                        return "Name is required and less than 30 characters.";
                    }
                }
                else if (propertyName == "Age")
                {
                    if (Age < 10 || Age > 50)
                    {
                        return "Age must between 10 and 50.";
                    }
                }
                return null;
            }
        }

        string IDataErrorInfo.Error
        {
            get
            {
                if (PersonName == "Tom" && Age != 30)
                {
                    return "Tom must be 30.";
                }
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
