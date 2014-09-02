using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CookMe.Common.Infrastructure {

    /// <summary>
    /// Represents ObservableObject, provides base implementation for INPC
    /// </summary>
    [Serializable]
    public abstract class ObservableObject : INotifyPropertyChanged {

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableObject"/> class.
        /// </summary>
        protected ObservableObject() {
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) {
            var handler = this.PropertyChanged;
            if (handler != null) {
                handler(this, e);
            }
        }

        /// <summary>
        /// Extracts property name from expression, verifies the property name exists, calls OnPropertyChanged.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpresssion">The property expresssion.</param>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpresssion) {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpresssion);
            this.RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Verifies the property name exists, calls OnPropertyChanged.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void RaisePropertyChanged(String propertyName) {
            VerifyPropertyName(propertyName);
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Warns the developer if this Object does not have a public property with
        /// the specified name. This method does not exist in a Release build.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(String propertyName) {
            Debug.Assert(this.GetType().GetProperty(propertyName) != null, "Invalid property name: " + propertyName);
        }
    }
}
