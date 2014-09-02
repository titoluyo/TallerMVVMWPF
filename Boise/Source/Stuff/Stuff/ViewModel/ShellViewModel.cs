
using System;
using System.Collections;
using Microsoft.Practices.Composite.Events;
using Stuff.Events;
using Stuff.Global;

namespace Stuff.ViewModel {

    /// <summary>
    /// Represents the ShellViewModel
    /// </summary>
    public class ShellViewModel : ViewModelBase {

        #region Declarations

        String _currentVisualState;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the state of the current visual.
        /// </summary>
        /// <value>The state of the current visual.</value>
        public String CurrentVisualState {
            get { return _currentVisualState; }
            set {
                _currentVisualState = value;
                RaisePropertyChanged("CurrentVisualState");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        public ShellViewModel() {
            var showAddStuffViewEvent = GetEvent<ShowAddStuffViewEvent>();
            showAddStuffViewEvent.Subscribe((ignore) => CurrentVisualState = Constants.STR_ADDING);
            var showBrowseStuffViewEvent = GetEvent<ShowBrowseStuffViewEvent>();
            showBrowseStuffViewEvent.Subscribe((ignore) => CurrentVisualState = Constants.STR_BROWSING);
            var showEditStuffViewEvent = GetEvent<ShowEditStuffViewEvent>();
            showEditStuffViewEvent.Subscribe((ignore) => CurrentVisualState = Constants.STR_EDITING);
            CurrentVisualState = Constants.STR_BROWSING;
        }

        #endregion
    }
}
