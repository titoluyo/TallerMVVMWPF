
using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Ocean.Wpf.Infrastructure;

namespace Ocean.Wpf.Controls {

    /// <summary>
    /// Represents FormNotification
    /// </summary>
    [TemplatePart(Name = "PART_Expander", Type = typeof(Expander))]
    public sealed class FormNotification : Control {

        #region  Declarations

        Expander _errorsExpander;
        AdornerLayer _errorsExpanderAdornerLayer;
        Timer _expandedTimer;
        TextBlockAdorner _textBlockAdorner;
        delegate void ExpanderDelegate();

        #endregion

        #region  Shared Properties

        /// <summary>
        /// Auto collapse timeout
        /// </summary>
        public static DependencyProperty AutoCollapseTimeoutProperty = DependencyProperty.Register("AutoCollapseTimeout", typeof(Double), typeof(FormNotification), new PropertyMetadata(2.0), IsAutoCollapseTimeoutValid);

        /// <summary>
        /// Error Header Foreground
        /// </summary>
        public static DependencyProperty ErrorHeaderForegroundProperty = DependencyProperty.Register("ErrorHeaderForeground", typeof(Brush), typeof(FormNotification), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 208, 0, 0))));

        /// <summary>
        /// Error Header Text
        /// </summary>
        public static DependencyProperty ErrorHeaderTextProperty = DependencyProperty.Register("ErrorHeaderText", typeof(String), typeof(FormNotification), new PropertyMetadata("Required Items"));

        /// <summary>
        /// Error Message
        /// </summary>
        public static DependencyProperty ErrorMessageProperty = DependencyProperty.Register("ErrorMessage", typeof(String), typeof(FormNotification), new PropertyMetadata(String.Empty, OnErrorMessageChanged));

        /// <summary>
        /// Error Pop Up Background
        /// </summary>
        public static DependencyProperty ErrorPopUpBackgroundProperty = DependencyProperty.Register("ErrorPopUpBackground", typeof(Brush), typeof(FormNotification), new PropertyMetadata(new SolidColorBrush(Color.FromArgb(255, 253, 240, 151))));

        /// <summary>
        /// Error Pop Up Foreground
        /// </summary>
        public static DependencyProperty ErrorPopUpForegroundProperty = DependencyProperty.Register("ErrorPopUpForeground", typeof(Brush), typeof(FormNotification), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Notification Message Background
        /// </summary>
        public static DependencyProperty NotificationMessageBackgroundProperty = DependencyProperty.Register("NotificationMessageBackground", typeof(Brush), typeof(FormNotification), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        /// <summary>
        /// Notification Message Foreground
        /// </summary>
        public static DependencyProperty NotificationMessageForegroundProperty = DependencyProperty.Register("NotificationMessageForeground", typeof(Brush), typeof(FormNotification), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));

        /// <summary>
        /// Notification Message
        /// </summary>
        public static DependencyProperty NotificationMessageProperty = DependencyProperty.Register("NotificationMessage", typeof(String), typeof(FormNotification), new PropertyMetadata(String.Empty, OnNotificationMessageChanged));

        /// <summary>
        /// Watermark Message Background
        /// </summary>
        public static DependencyProperty WatermarkMessageBackgroundProperty = DependencyProperty.Register("WatermarkMessageBackground", typeof(Brush), typeof(FormNotification));

        /// <summary>
        /// Watermark Message Foreground
        /// </summary>
        public static DependencyProperty WatermarkMessageForegroundProperty = DependencyProperty.Register("WatermarkMessageForeground", typeof(Brush), typeof(FormNotification), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));

        /// <summary>
        /// Watermark Message
        /// </summary>
        public static DependencyProperty WatermarkMessageProperty = DependencyProperty.Register("WatermarkMessage", typeof(String), typeof(FormNotification), new PropertyMetadata(String.Empty));

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the auto collapse timeout.
        /// </summary>
        /// <value>The auto collapse timeout.</value>
        [Category("Custom"), Description("Number of seconds the error pop remains expanded after the mouse leaves.  Default is 2 seconds.  Example 2.5 is 2 1/2 seconds.  Valid range is 0 - 100.  Zero means Auto Collapse is turned off.")]
        public Double AutoCollapseTimeout {
            get {
                return Convert.ToDouble(GetValue(AutoCollapseTimeoutProperty));
            }
            set {
                SetValue(AutoCollapseTimeoutProperty, value);

                if(_expandedTimer != null) {
                    _expandedTimer.Interval = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the error header foreground.
        /// </summary>
        /// <value>The error header foreground.</value>
        [Category("Custom"), Description("Error header text foreground brush.")]
        public Brush ErrorHeaderForeground {
            get {
                return (Brush)(GetValue(ErrorHeaderForegroundProperty));
            }
            set {
                SetValue(ErrorHeaderForegroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the error header text.
        /// </summary>
        /// <value>The error header text.</value>
        [Category("Custom"), Description("Error header text that is displayed when there is an error on the form.  Setting this text property causes it to be displayed.  Normally this property is data bound to the Error property on an object data implements the IDataErrorInfo interface.")]
        public String ErrorHeaderText {
            get {
                return Convert.ToString(GetValue(ErrorHeaderTextProperty));
            }
            set {
                SetValue(ErrorHeaderTextProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        [Category("Custom"), Description("Error message that is displayed in the expander pop up.")]
        public String ErrorMessage {
            get {
                return Convert.ToString(GetValue(ErrorMessageProperty));
            }
            set {
                SetValue(ErrorMessageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the error pop up background.
        /// </summary>
        /// <value>The error pop up background.</value>
        [Category("Custom"), Description("Error message pop up background brush.")]
        public Brush ErrorPopUpBackground {
            get {
                return (Brush)(GetValue(ErrorPopUpBackgroundProperty));
            }
            set {
                SetValue(ErrorPopUpBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the error pop up foreground.
        /// </summary>
        /// <value>The error pop up foreground.</value>
        [Category("Custom"), Description("Error message pop up forground brush.")]
        public Brush ErrorPopUpForeground {
            get {
                return (Brush)(GetValue(ErrorPopUpForegroundProperty));
            }
            set {
                SetValue(ErrorPopUpForegroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the notification message.
        /// </summary>
        /// <value>The notification message.</value>
        [Category("Custom"), Description("Notification message text.  If this property is set and their is no error message text, this text will be displayed.  This is normally set in after successful write or delete operations by the host form.")]
        public String NotificationMessage {
            get {
                return Convert.ToString(GetValue(NotificationMessageProperty));
            }
            set {
                SetValue(NotificationMessageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the notification message background.
        /// </summary>
        /// <value>The notification message background.</value>
        [Category("Custom"), Description("Notification message pop up background brush.")]
        public Brush NotificationMessageBackground {
            get {
                return (Brush)(GetValue(NotificationMessageBackgroundProperty));
            }
            set {
                SetValue(NotificationMessageBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the notification message foreground.
        /// </summary>
        /// <value>The notification message foreground.</value>
        [Category("Custom"), Description("Notification message pop up foreground brush.")]
        public Brush NotificationMessageForeground {
            get {
                return (Brush)(GetValue(NotificationMessageForegroundProperty));
            }
            set {
                SetValue(NotificationMessageForegroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the watermark message.
        /// </summary>
        /// <value>The watermark message.</value>
        [Category("Custom"), Description("Watermark text message.  This is displayed if there is no error message text or notification message text.")]
        public String WatermarkMessage {
            get {
                return Convert.ToString(GetValue(WatermarkMessageProperty));
            }
            set {
                SetValue(WatermarkMessageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the watermark message background.
        /// </summary>
        /// <value>The watermark message background.</value>
        [Category("Custom"), Description("Watermark message pop up background brush.")]
        public Brush WatermarkMessageBackground {
            get {
                return (Brush)(GetValue(WatermarkMessageBackgroundProperty));
            }
            set {
                SetValue(WatermarkMessageBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the watermark message foreground.
        /// </summary>
        /// <value>The watermark message foreground.</value>
        [Category("Custom"), Description("Watermark message pop up foreground brush.")]
        public Brush WatermarkMessageForeground {
            get {
                return (Brush)(GetValue(WatermarkMessageForegroundProperty));
            }
            set {
                SetValue(WatermarkMessageForegroundProperty, value);
            }
        }

        #endregion

        #region  Constructor and Initializer

        /// <summary>
        /// Initializes a new instance of the <see cref="FormNotification"/> class.
        /// </summary>
        public FormNotification() {
            this.Initialized += FormNotification_Initialized;
        }

        /// <summary>
        /// Initializes the <see cref="FormNotification"/> class.
        /// </summary>
        static FormNotification() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FormNotification), new FrameworkPropertyMetadata(typeof(FormNotification)));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        void FormNotification_Initialized(object sender, EventArgs e) {
            _expandedTimer = new Timer(AutoCollapseTimeout * 1000) {Enabled = false, AutoReset = false};
            _expandedTimer.Elapsed += _objExpandedTimer_Elapsed;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// When the expander collapses, need to remove the TextBlock adorner
        /// </summary>
        void _objErrorsExpander_Collapsed(object sender, RoutedEventArgs e) {

            if(_errorsExpanderAdornerLayer != null) {
                _textBlockAdorner.MouseLeave -= _objTextBlockAdorner_MouseLeave;
                _textBlockAdorner.MouseEnter -= _objTextBlockAdorner_MouseEnter;
                _errorsExpanderAdornerLayer.Remove(_textBlockAdorner);
                _textBlockAdorner = null;
                _errorsExpanderAdornerLayer = null;
            }

        }

        /// <summary>
        /// When the expander expands, need to put the error message in the
        /// adorner layer because the controls below it, may have their own
        /// adorner validation error messages, this places the expander
        /// popup on top of all other adorder layer elements. 
        /// </summary>
        void _objErrorsExpander_Expanded(object sender, RoutedEventArgs e) {
            //this forces the ErrorMessage to be reread from it's source
            this.InvalidateProperty(ErrorMessageProperty);

            var expanderGrid = VisualTreeSearchAssistant.FindVisualChild<Grid>(_errorsExpander);

            _errorsExpanderAdornerLayer = AdornerLayer.GetAdornerLayer(expanderGrid);

            String[] delimiter = { Environment.NewLine };
            String[] errorMessages = this.ErrorMessage.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            Array.Sort(errorMessages);

            var txt = new TextBlock {Width = this.Width, TextWrapping = TextWrapping.Wrap, Text = string.Join(Environment.NewLine + Environment.NewLine, errorMessages), Padding = new Thickness(5), Foreground = this.ErrorPopUpForeground, Background = this.ErrorPopUpBackground, Effect = new System.Windows.Media.Effects.DropShadowEffect {ShadowDepth = 2}};

            //need to move the TextBlock down below the expander and indent a little
            var obj = new TranslateTransform(5, _errorsExpander.ActualHeight + 2);
            txt.RenderTransform = obj;
            _textBlockAdorner = new TextBlockAdorner(expanderGrid, txt);
            _textBlockAdorner.MouseLeave -= _objTextBlockAdorner_MouseLeave;
            _textBlockAdorner.MouseEnter -= _objTextBlockAdorner_MouseEnter;
            _errorsExpanderAdornerLayer.Add(_textBlockAdorner);
        }

        void _objErrorsExpander_MouseEnter(Object sender, System.Windows.Input.MouseEventArgs e) {
            _expandedTimer.Stop();
        }

        void _objErrorsExpander_MouseLeave(Object sender, System.Windows.Input.MouseEventArgs e) {
            if(_expandedTimer.Interval > 0) {
                _expandedTimer.Start();
            }
        }

        /// <summary>
        /// Since WPF uses the STA threading model, another thread like a timer
        /// can not update the UI.  WPF provides a very simple technique for
        /// updating the UI from another thread.
        /// </summary>
        void _objExpandedTimer_Elapsed(Object sender, ElapsedEventArgs e) {
            Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, new ExpanderDelegate(CloseExpander));
        }

        void _objTextBlockAdorner_MouseEnter(Object sender, System.Windows.Input.MouseEventArgs e) {
            _expandedTimer.Stop();
        }

        void _objTextBlockAdorner_MouseLeave(Object sender, System.Windows.Input.MouseEventArgs e) {
            if(_expandedTimer.Interval > 0) {
                _expandedTimer.Start();
            }
        }

        /// <summary>
        /// This method is called when the error message or notification message
        /// property values are set and when the Time.Elapsed event fires.
        /// This ensures that the expander region is closed and the adorner layer
        /// is removed.
        /// </summary>
        void CloseExpander() {
            if(_errorsExpander != null && _errorsExpander.IsExpanded) {
                _errorsExpander.IsExpanded = false;
            }
        }

        /// <summary>
        /// This method is called by the WPF Dependency Property system when the
        /// AutoCollapseTimeout value is set.
        /// </summary>
        public static Boolean IsAutoCollapseTimeoutValid(Object value) {
            Double dbl = Convert.ToDouble(value);
            return dbl >= 0 && dbl <= 100;
        }

        /// <summary>
        /// This is where you can get a reference to a control 
        /// inside the control template.  Notice the PART_ naming convention.
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            //
            //Each object that you are getting a reference to here is also
            //listed in a TemplatePart attribute on the class.
            //
            //<TemplatePart(Name:="PART_Expander", Type:=GetType(Expander))> _
            //
            _errorsExpander = (GetTemplateChild("PART_Expander")) as Expander;
            if (_errorsExpander != null) {
                _errorsExpander.MouseLeave += _objErrorsExpander_MouseLeave;
                _errorsExpander.MouseEnter += _objErrorsExpander_MouseEnter;
                _errorsExpander.Expanded += _objErrorsExpander_Expanded;
                _errorsExpander.Collapsed += _objErrorsExpander_Collapsed;
            } else {
                throw new InvalidOperationException("PART_Expander is missing");
            }
        }

        /// <summary>
        /// This is the call back that gets called when the ErrorMessage
        /// dependency property is changed.
        /// </summary>
        static void OnErrorMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {

            var obj = (FormNotification)d;

            if(!(string.IsNullOrEmpty(e.NewValue.ToString()))) {
                obj.NotificationMessage = String.Empty;
            }

            obj.CloseExpander();
        }

        /// <summary>
        /// This is the call back that gets called when the NotificationMessage
        /// dependency property is changed.
        /// </summary>
        static void OnNotificationMessageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((FormNotification)d).CloseExpander();
        }

        #endregion
    }
}