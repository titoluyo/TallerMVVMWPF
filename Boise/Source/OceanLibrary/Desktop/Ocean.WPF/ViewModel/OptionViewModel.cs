using System;
using Ocean.Infrastructure;

namespace Ocean.Wpf.ViewModel {

    /// <summary>
    /// Represents OptionViewModel.  This is a wrapper for data that will be displayed in an ItemsControl. 
    /// Has built-in sorting and also provides custom sorting capabilities.
    /// Introducted in Karl and Josh's Internationalized MVVM Wizard article on CodeProject. 
    /// </summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class OptionViewModel<TValue> : ObservableObject, IComparable<OptionViewModel<TValue>> {

        #region  Declarations

        const Int32 _UNSET_SORT_VALUE = Int32.MinValue;
        Boolean _isSelected;
        readonly Int32 _sortValue = Int32.MinValue;
        readonly TValue _value;
        readonly String _displayName = string.Empty;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public String DisplayName {
            get {
                return _displayName;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsSelected {
            get {
                return _isSelected;
            }
            set {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        /// <summary>
        /// Gets the sort value.
        /// </summary>
        /// <value>The sort value.</value>
        public Int32 SortValue {
            get {
                return _sortValue;
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>The value.</value>
        public TValue Value {
            get {
                return _value;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionViewModel&lt;TValue&gt;"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="value">The value.</param>
        public OptionViewModel(String displayName, TValue value) {
            _displayName = displayName;
            _value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionViewModel&lt;TValue&gt;"/> class.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="value">The value.</param>
        /// <param name="sortValue">The sort value.</param>
        public OptionViewModel(String displayName, TValue value, Int32 sortValue) {
            _displayName = displayName;
            _value = value;
            _sortValue = sortValue;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public Int32 CompareTo(OptionViewModel<TValue> other) {
            if(other == null) {
                return -1;
            }

            if(_sortValue == _UNSET_SORT_VALUE && other.SortValue == _UNSET_SORT_VALUE) {
                return _displayName.CompareTo(other.DisplayName);
            }

            if(_sortValue != _UNSET_SORT_VALUE && other.SortValue != _UNSET_SORT_VALUE) {
                return _sortValue.CompareTo(other.SortValue);
            }

            if(_sortValue != _UNSET_SORT_VALUE && other.SortValue == _UNSET_SORT_VALUE) {
                return -1;
            }

            return +1;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>TValue</returns>
        public TValue GetValue() {
            return _value;
        }

        #endregion
    }
}

