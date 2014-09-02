using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.Practices.Prism.ViewModel;
using WPFPrismv4Navigation.Properties;

namespace WPFPrismv4Navigation.Infrastructure {

    /// <summary>
    /// Base class with rich capabilities for raising property change notification.
    /// Notice this take a dependency on the Prism assembly.
    /// </summary>
#if SILVERLIGHT
#else
    [Serializable]
    public abstract class ObservableObject : INotifyPropertyChanged {
#endif


#if SILVERLIGHT
#else
        [field: NonSerialized]
#endif
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
            var handler = this.PropertyChanged;
            if(handler != null) {
                handler(this, e);
            }
        }

        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpresssion) {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpresssion);
            this.RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged(String propertyName) {
            VerifyPropertyName(propertyName);
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanged(params String[] propertyNames) {
            const String STR_PROPERTYNAMES = "propertyNames";

            if(propertyNames == null) {
                throw new ArgumentNullException(STR_PROPERTYNAMES);
            }

            if(propertyNames == null) {
                throw new ArgumentOutOfRangeException(STR_PROPERTYNAMES, Resources.Exception_ArrayEmpty);
            }

            foreach(var propertyName in propertyNames) {
                this.RaisePropertyChanged(propertyName);
            }
        }

        /// <summary>
        /// Warns the developer if this object does not have a public property with
        /// the specified name. This method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(String propertyName) {
            // verify that the property name matches a real,  
            // public, instance property on this object.

#if SILVERLIGHT
            Debug.Assert(GetType().GetProperty(propertyName) != null, "Invalid property name: " + propertyName);
#else
            if(TypeDescriptor.GetProperties(this)[propertyName] == null) {
                Debug.Fail("Invalid property name: " + propertyName);
            }
#endif
        }
    }
}

