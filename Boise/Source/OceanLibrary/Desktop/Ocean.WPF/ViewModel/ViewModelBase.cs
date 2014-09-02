using System;
using System.Windows;
using System.Windows.Threading;
using Ocean.Infrastructure;

namespace Ocean.Wpf.ViewModel {

    /// <summary>
    /// Represents ViewModelBase
    /// </summary>
    [Serializable]
    public abstract class ViewModelBase : ObservableObject {

        String _viewModelFriendlyName;

        /// <summary>
        /// Gets the dispatcher.  Works at run-time and test-time.
        /// </summary>
        /// <value>The dispatcher.</value>
        protected Dispatcher Dispatcher {
            get { return Application.Current != null ? Application.Current.Dispatcher : Dispatcher.CurrentDispatcher; }
        }

        /// <summary>
        /// Gets or sets the View Model Friendly Name.  Default value is the type name.  Consumed by the UI in some applications.
        /// </summary>
        /// <value>The name of ViewModelFriendlyName.</value>
        public String ViewModelFriendlyName {
            get { return _viewModelFriendlyName; }
            set {
                _viewModelFriendlyName = value;
                RaisePropertyChanged("ViewModelFriendlyName");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        protected ViewModelBase() {
            this.ViewModelFriendlyName = this.GetType().Name;
        }
    }
}
