using System;
using System.Collections.Generic;
using System.Text;
using Wpf.Common.Infrastructure;

namespace Wpf.Validation.Infrastructure {

    /// <summary>
    /// Represents the MaintenanceFormViewModelBase.  
    /// Provides UI Validation Error Managment; keeps the view model informed of all exceptions thrown in the databinding pipeline in the view and view model.
    /// Provides properties for surfacing validation and databinding pipeline exceptions to the view.
    /// </summary>
    public abstract class MaintenanceFormViewModelBase : ObservableObject {

        #region Declarations

        readonly IDictionary<String, ViewValidationError> _viewValidationErrorDictionary = new Dictionary<String, ViewValidationError>();

        #endregion //Declarations

        #region Properties

        /// <summary>
        /// Gets the view validation error count.
        /// </summary>
        /// <value>The view validation error count.</value>
        public Int32 ViewValidationErrorCount {
            get { return _viewValidationErrorDictionary.Count; }
        }

        /// <summary>
        /// Gets the view validation error messages.
        /// </summary>
        /// <value>The view validation error messages.</value>
        public String ViewValidationErrorMessages {
            get {
                if (this.ViewValidationErrorCount == 0) {
                    return String.Empty;
                }

                var sb = new StringBuilder();
                foreach (var kvp in _viewValidationErrorDictionary) {
                    sb.AppendLine(kvp.Value.ToFriendlyErrorMessage());
                }
                return sb.ToString();
            }
        }

        #endregion //Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MaintenanceFormViewModelBase"/> class.
        /// </summary>
        protected MaintenanceFormViewModelBase() {

        }

        #endregion //Constructor

        #region ViewValidationError Methods

        /// <summary>
        /// Adds the view validation error.
        /// </summary>
        /// <param name="e">The e <see cref="ViewValidationError"/></param>
        public void AddViewValidationError(ViewValidationError e) {
            _viewValidationErrorDictionary.Add(e.Key, e);
            RaisePropertyChanged("ViewValidationErrorMessages");
            RaisePropertyChanged("ViewValidationErrorCount");
        }

        /// <summary>
        /// Clears all view validation errors.
        /// </summary>
        protected void ClearViewValidationErrors() {
            _viewValidationErrorDictionary.Clear();
            RaisePropertyChanged("ViewValidationErrorMessages");
            RaisePropertyChanged("ViewValidationErrorCount");
        }

        /// <summary>
        /// Removes the view validation error.
        /// </summary>
        /// <param name="e">The e <see cref="ViewValidationError"/></param>
        public void RemoveViewValidationError(ViewValidationError e) {
            _viewValidationErrorDictionary.Remove(e.Key);
            RaisePropertyChanged("ViewValidationErrorMessages");
            RaisePropertyChanged("ViewValidationErrorCount");
        }

        #endregion // ViewValidationError Methods
    }
}
