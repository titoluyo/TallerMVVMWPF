using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ocean.Wpf.Controls {

    /// <summary>
    /// An extended WPF ComboBox that works like the ASP.NET AJAX enabled ComboBoxes
    /// </summary>
    public class AjaxStyleComboBox : ComboBox {

        #region Declarations

        Boolean _keyBoardActivity;
        Int32 _minimunSearchTextLength = 1;

        #endregion

        #region  Properties

        /// <summary>
        /// What is the minimum number of characters the user must enter before they are allowed to request a record search
        /// </summary>
        [Description("What is the minimum number of characters the user must enter before they are allowed to request a record search"), Category("Custom"), DefaultValue(1)]
        public int MinimunSearchTextLength {
            get {
                return _minimunSearchTextLength;
            }
            set {
                _minimunSearchTextLength = value;
            }
        }

        #endregion

        #region  RoutedEvents

        /// <summary>
        /// Fires when a record is selected.  This replaces the ComboBox SelectedChanged event
        /// </summary>
        public static readonly RoutedEvent LoadItemsSourceRoutedEvent = EventManager.RegisterRoutedEvent("LoadItemsSource", RoutingStrategy.Bubble, typeof(LoadItemsSourceRoutedEventHandler), typeof(AjaxStyleComboBox));

        /// <summary>
        /// Fires when the combo box needs its items loaded
        /// </summary>
        public static readonly RoutedEvent RecordSelectedRoutedEvent = EventManager.RegisterRoutedEvent("RecordSelected", RoutingStrategy.Bubble, typeof(RecordSelectedRoutedEventHandler), typeof(AjaxStyleComboBox));

        /// <summary>
        /// Occurs when load items source.
        /// </summary>
        public event LoadItemsSourceRoutedEventHandler LoadItemsSource {
            add {
                this.AddHandler(LoadItemsSourceRoutedEvent, value);
            }
            remove {
                this.RemoveHandler(LoadItemsSourceRoutedEvent, value);
            }
        }

        /// <summary>
        /// Handles the RaiseEvent event of the LoadItemsSource control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Ocean.Wpf.Controls.LoadItemsSourceRoutedEventArgs"/> instance containing the event data.</param>
        public void LoadItemsSource_RaiseEvent(Object sender, LoadItemsSourceRoutedEventArgs e) {
            this.RaiseEvent(e);
        }

        /// <summary>
        /// Occurs when [record selected].
        /// </summary>
        public event RecordSelectedRoutedEventHandler RecordSelected {
            add {
                this.AddHandler(RecordSelectedRoutedEvent, value);
            }
            remove {
                this.RemoveHandler(RecordSelectedRoutedEvent, value);
            }
        }
        /// <summary>
        /// Handles the RaiseEvent event of the RecordSelected control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Ocean.Wpf.Controls.RecordSelectedRoutedEventArgs"/> instance containing the event data.</param>
        public void RecordSelected_RaiseEvent(Object sender, RecordSelectedRoutedEventArgs e) {
            this.RaiseEvent(e);
        }

        #endregion

        #region  Methods

        /// <summary>
        /// Clears all.
        /// </summary>
        public void ClearAll() {
            this.ItemsSource = null;
            this.Text = string.Empty;
            this.IsDropDownOpen = false;
        }

        /// <summary>
        /// Raises the LoadItemsSource event when the user has entered a search string and pressed ENTER
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e) {
            _keyBoardActivity = true;

            //this code only runs if the DropDown is not open because if its open the OnPreviewKeyDown code will handle the ENTER key
            if(this.Text.Trim().Length >= this.MinimunSearchTextLength && e.Key == Key.Enter) {
                OnLoadItemsSource(new LoadItemsSourceRoutedEventArgs(LoadItemsSourceRoutedEvent, this, this.Text));
                this.IsDropDownOpen = true;
                e.Handled = true;
                this.SelectedIndex = -1;

            } else {
                base.OnKeyDown(e);
            }

        }

        /// <summary>
        /// Raises the <see cref="LoadItemsSource"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Ocean.Wpf.Controls.LoadItemsSourceRoutedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnLoadItemsSource(LoadItemsSourceRoutedEventArgs e) {
            LoadItemsSource_RaiseEvent(this, e);
            //Karl commented this out so that a record would not be loaded 
            //on the host form until the user actually selected a record.
            //Me.RaiseEvent(New RecordSelectedRoutedEventArgs(AjaxStyleComboBox.RecordSelectedRoutedEvent, Me, Me.SelectedValue))
        }

        /// <summary>
        /// Raises the RecordSelected event if the user presses ENTER or TAB
        /// </summary>
        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            _keyBoardActivity = true;

            if(this.IsDropDownOpen && this.SelectedIndex > -1 && (e.Key == Key.Enter || e.Key == Key.Tab)) {
                OnRecordSelected(new RecordSelectedRoutedEventArgs(RecordSelectedRoutedEvent, this, this.SelectedValue));
                this.IsDropDownOpen = false;
                e.Handled = true;

            } else if(e.Key == Key.Escape) {
                ClearAll();
                e.Handled = true;

            } else {
                base.OnPreviewKeyDown(e);
            }
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="System.Windows.UIElement.PreviewMouseLeftButtonDown"/> routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that the left mouse button was pressed.</param>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e) {
            _keyBoardActivity = false;
            base.OnPreviewMouseLeftButtonDown(e);
        }

        /// <summary>
        /// Raises the <see cref="RecordSelected"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Ocean.Wpf.Controls.RecordSelectedRoutedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnRecordSelected(RecordSelectedRoutedEventArgs e) {
            RecordSelected_RaiseEvent(this, e);
        }

        /// <summary>
        /// Raises the RecordSelected event if the user selects a record with their mouse.
        /// </summary>
        protected override void OnSelectionChanged(SelectionChangedEventArgs e) {
            base.OnSelectionChanged(e);

            if(_keyBoardActivity == false) {
                OnRecordSelected(new RecordSelectedRoutedEventArgs(RecordSelectedRoutedEvent, this, this.SelectedValue));
            }
        }

        #endregion
    }
}

