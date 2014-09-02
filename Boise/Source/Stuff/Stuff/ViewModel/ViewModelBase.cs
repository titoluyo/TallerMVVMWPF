
using System;
using System.ComponentModel;
using Microsoft.Practices.Composite.Events;
using Simple.Core.Services.Dialog;
using Simple.Core.Services.Container;
using Stuff.Services.DataStore;
using System.Windows;
using Stuff.Services.Container;

namespace Stuff.ViewModel {

    /// <summary>
    /// Represents a ViewModelBase.  This is the base class for all ViewModels, providing wrappers for services and INotifyPropertyChanged implementation.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged {

        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the IDialogService registered with the ServiceContainer.
        /// </summary>
        protected IDialogService Dialog {
            get { return GetService<IDialogService>(); }
        }

        /// <summary>
        /// Gets the IEventAggregator registered with the ServiceContainer.
        /// </summary>
        protected IEventAggregator EventAggregator {
            get { return GetService<IEventAggregator>(); }
        }

        /// <summary>
        /// Gets the IMovieDataStoreService registered with the ServiceContainer.
        /// </summary>
        protected IMovieDataStoreService MovieDataStoreService {
            get { return GetService<IMovieDataStoreService>(); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes the <see cref="ViewModelBase"/> class.
        /// </summary>
        static ViewModelBase() {

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) {
                ServiceLoader.LoadDesignTimeServices();
            }
        }

        #endregion

        #region Methods

        protected T GetEvent<T>() where T : EventBase {
            return EventAggregator.GetEvent<T>();
        }

        /// <summary>
        /// Gets the requested service.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of Service to return</typeparam>
        protected T GetService<T>() where T : class {
            return ServiceContainer.Instance.GetService<T>();
        }

        /// <summary>
        /// Raises the property changed event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void RaisePropertyChanged(String propertyName) {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
