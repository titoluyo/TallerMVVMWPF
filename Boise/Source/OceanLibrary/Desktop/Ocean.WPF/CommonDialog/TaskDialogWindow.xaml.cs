using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media.Animation;
using Ocean.Wpf.Aero;

namespace Ocean.Wpf.CommonDialog {

    /// <summary>
    /// Represents TaskDialogWindow
    /// </summary>
    public partial class TaskDialogWindow : Window {

        #region Declarations

        Boolean _aeroGlassEnabled;
        TaskDialogResult _taskDialogResult = TaskDialogResult.None;
        readonly Int32 _buttonsDisabledDelay;
        Timer _buttonDelayTimer;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the custom dialog result.
        /// </summary>
        /// <value>The custom dialog result.</value>
        public TaskDialogResult TaskDialogResult {
            get {
                return _taskDialogResult;
            }
        }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskDialogWindow"/> class.
        /// </summary>
        /// <param name="buttonsDisabledDelay">The buttons disabled delay.</param>
        public TaskDialogWindow(Int32 buttonsDisabledDelay) {
            InitializeComponent();

            if(Environment.OSVersion.Version.Major < 6) {
                this.AllowsTransparency = true;
                _aeroGlassEnabled = false;

            } else {
                _aeroGlassEnabled = true;
            }

            _buttonsDisabledDelay = buttonsDisabledDelay;
            this.tbCaption.MouseLeftButtonDown += tbCaption_MouseLeftButtonDown;
            this.expAdditionalDetails.Expanded += expAdditionalDetails_Expanded;
            this.expAdditionalDetails.Collapsed += expAdditionalDetails_Collapsed;
            this.Loaded += CustomDialogWindow_Loaded;
            this.Closing += CustomDialogWindow_Closing;
            this.btnYes.Click += btnYes_Click;
            this.btnOK.Click += btnOK_Click;
            this.btnNo.Click += btnNo_Click;
            this.btnCancel.Click += btnCancel_Click;
        }

        #endregion

        #region  Methods

        void btnCancel_Click(object sender, RoutedEventArgs e) {
            _taskDialogResult = TaskDialogResult.Cancel;
            this.DialogResult = true;
        }

        void btnNo_Click(object sender, RoutedEventArgs e) {
            _taskDialogResult = TaskDialogResult.No;
            this.DialogResult = true;
        }

        void btnOK_Click(object sender, RoutedEventArgs e) {
            _taskDialogResult = TaskDialogResult.Ok;
            this.DialogResult = true;
        }

        void btnYes_Click(object sender, RoutedEventArgs e) {
            _taskDialogResult = TaskDialogResult.Yes;
            this.DialogResult = true;
        }

        void CustomDialogWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e) {

            //this prevents ALT-F4 from closing the dialog box
            if(this.DialogResult.HasValue && this.DialogResult.Value) {
                e.Cancel = false;

            } else {
                e.Cancel = true;
            }
        }

        void CustomDialogWindow_Loaded(object sender, RoutedEventArgs e) {
            this.tbAdditionalDetailsText.Visibility = Visibility.Collapsed;

            if(this.ResizeMode != ResizeMode.NoResize) {
                //this work around is necessary when glass is enabled and the window style is None which removes the chrome because the resize mode MUST be set to CanResize or else glass won't display
                this.MinHeight = this.ActualHeight;
                this.MaxHeight = this.ActualHeight;
                this.MinWidth = this.ActualWidth;
                this.MaxWidth = this.ActualWidth;
            }

            if(_buttonsDisabledDelay > 0) {
                this.pbDisabledButtonsProgressBar.Maximum = _buttonsDisabledDelay;
                this.pbDisabledButtonsProgressBar.IsIndeterminate = false;

                var duration = new Duration(TimeSpan.FromSeconds(_buttonsDisabledDelay));
                var doubleAnimation = new DoubleAnimation(_buttonsDisabledDelay, duration);
                this.pbDisabledButtonsProgressBar.BeginAnimation(RangeBase.ValueProperty, doubleAnimation);
                this.btnCancel.IsEnabled = false;
                this.btnNo.IsEnabled = false;
                this.btnOK.IsEnabled = false;
                this.btnYes.IsEnabled = false;
                _buttonDelayTimer = new Timer();
                _buttonDelayTimer.Tick += OnTimedEvent;
                _buttonDelayTimer.Interval = _buttonsDisabledDelay * 1000;
                _buttonDelayTimer.Start();

            } else {
                this.pbDisabledButtonsProgressBar.Visibility = Visibility.Collapsed;
            }
        }

        void expAdditionalDetails_Collapsed(Object sender, RoutedEventArgs e) {
            this.expAdditionalDetails.Header = Properties.Resources.CustomTaskDialogWindow_expAdditionalDetails_Collapsed_See_Details;
            this.tbAdditionalDetailsText.Visibility = Visibility.Collapsed;
            this.UpdateLayout();

            if(this.ResizeMode != ResizeMode.NoResize) {
                this.MaxHeight = this.ActualHeight;
            }
        }

        void expAdditionalDetails_Expanded(object sender, RoutedEventArgs e) {

            if(this.ResizeMode != ResizeMode.NoResize) {
                this.MaxHeight = Double.PositiveInfinity;
            }

            this.expAdditionalDetails.Header = Properties.Resources.CustomTaskDialogWindow_expAdditionalDetails_Expanded_Hide_Details;
            this.tbAdditionalDetailsText.Visibility = Visibility.Visible;
            this.UpdateLayout();

            if(this.ResizeMode != ResizeMode.NoResize) {
                this.MaxHeight = this.ActualHeight;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Window.SourceInitialized"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        protected override void OnSourceInitialized(EventArgs e) {
            base.OnSourceInitialized(e);

            if(_aeroGlassEnabled == false) {
                //no aero glass
                this.ResizeMode = ResizeMode.CanResize;
                this.borderCustomDialog.Background = SystemColors.ActiveCaptionBrush;
                this.tbCaption.Foreground = SystemColors.ActiveCaptionTextBrush;
                this.borderCustomDialog.CornerRadius = new CornerRadius(10, 10, 0, 0);
                this.borderCustomDialog.Padding = new Thickness(4, 0, 4, 4);
                this.borderCustomDialog.BorderThickness = new Thickness(0, 0, 1, 1);
                this.borderCustomDialog.BorderBrush = System.Windows.Media.Brushes.Black;

            } else {

                //aero glass
                if(VistaAeroApi.ExtendGlassFrame(this, new Thickness(0, 25, 0, 0)) == false) {
                    //aero didn't work make window without glass
                    this.borderCustomDialog.Background = SystemColors.ActiveCaptionBrush;
                    this.tbCaption.Foreground = SystemColors.ActiveCaptionTextBrush;
                    this.borderCustomDialog.Padding = new Thickness(4, 0, 4, 4);
                    this.borderCustomDialog.BorderThickness = new Thickness(0, 0, 1, 1);
                    this.borderCustomDialog.BorderBrush = System.Windows.Media.Brushes.Black;
                    _aeroGlassEnabled = false;
                }
            }

            this.ResizeMode = ResizeMode.CanResize;
        }

        void OnTimedEvent(object source, EventArgs e) {
            _buttonDelayTimer.Stop();
            _buttonDelayTimer.Dispose();
            _buttonDelayTimer = null;
            this.btnCancel.IsEnabled = true;
            this.btnNo.IsEnabled = true;
            this.btnOK.IsEnabled = true;
            this.btnYes.IsEnabled = true;
            this.pbDisabledButtonsProgressBar.Visibility = Visibility.Collapsed;
        }

        void tbCaption_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            DragMove();
        }

        #endregion
    }
}

