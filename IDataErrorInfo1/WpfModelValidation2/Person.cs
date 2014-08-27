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
        public int Age {
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

        public string ModelError
        {
            get { return ModelValidation(); }
        }

        private string ModelValidation()
        {
            if (PersonName == "Tom" && Age != 30)
            {
                return "Tom must be 30.";
            }
            return null;
        }

        //Call this to trigger the model validation.
        public void Validate()
        {
            NotifyPropertyChanged("");
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                if (propertyName=="ModelError")
                {
                    string strValidationMessage = ModelValidation();
                    if (!string.IsNullOrEmpty(strValidationMessage))
                    {
                        return strValidationMessage;
                    }
                }

                if(propertyName=="PersonName")
                {
                    if(PersonName.Length>30 || PersonName.Length<1)
                    {
                        return "Name is required and less than 30 characters.";
                    }
                }
                else if (propertyName == "Age")
                {
                    if (Age<10 || Age>50)
                    {
                        return "Age must between 10 and 50.";
                    }
                }
                return null;
            }
        }

        string IDataErrorInfo.Error
        {
            get { throw new NotImplementedException(); }
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
