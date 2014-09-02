
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ocean.Wpf.Controls {

    /// <summary>
    /// Represents a Search Entry control
    /// </summary>
    [StyleTypedProperty(Property = _FILTERTEXTBOXSTYLE_NAME, StyleTargetType = typeof(SearchEntry))]
    [StyleTypedProperty(Property = _FILTERRESETBUTTONSTYLE_NAME, StyleTargetType = typeof(SearchEntry))]
    [StyleTypedProperty(Property = _FILTERBORDERSTYLE_NAME, StyleTargetType = typeof(SearchEntry))]
    [TemplatePart(Name = _FILTERTEXTBOX_NAME, Type = typeof(TextBox))]
    [TemplatePart(Name = _FILTERRESETBUTTON_NAME, Type = typeof(Button))]
    public class SearchEntry : Control {

        #region Declarations

        const String _CATEGORY_NAME = "Custom";
        const String _FILTERBORDERSTYLE_NAME = "FilterBorderStyle";
        //all strings used more than once have a constant assigned.
        const String _FILTERRESETBUTTON_NAME = "PART_FilterResetButton";
        const String _FILTERRESETBUTTONSTYLE_NAME = "FilterResetButtonStyle";
        const String _FILTERTEXTBOX_NAME = "PART_FilterTextBox";
        const String _FILTERTEXTBOXSTYLE_NAME = "FilterTextBoxStyle";
        Button _filterResetButton;
        TextBox _filterTextBox;

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Access Key Text
        /// </summary>
        public static readonly DependencyProperty AccessKeyTextProperty = DependencyProperty.Register("AccessKeyText", typeof(String), typeof(SearchEntry), new UIPropertyMetadata(String.Empty));

        /// <summary>
        /// Filter Border Style
        /// </summary>
        public static readonly DependencyProperty FilterBorderStyleProperty = DependencyProperty.Register(_FILTERBORDERSTYLE_NAME, typeof(Style), typeof(SearchEntry), null);

        /// <summary>
        /// Filter Reset Button Style
        /// </summary>
        public static readonly DependencyProperty FilterResetButtonStyleProperty = DependencyProperty.Register(_FILTERRESETBUTTONSTYLE_NAME, typeof(Style), typeof(SearchEntry), null);

        /// <summary>
        /// Filter TextBox Style
        /// </summary>
        public static readonly DependencyProperty FilterTextBoxStyleProperty = DependencyProperty.Register(_FILTERTEXTBOXSTYLE_NAME, typeof(Style), typeof(SearchEntry), null);

        /// <summary>
        /// Filter TextBox WaterMark Text 
        /// </summary>
        public static readonly DependencyProperty FilterTextBoxWaterMarkTextProperty = DependencyProperty.Register("FilterTextBoxWaterMarkText", typeof(String), typeof(SearchEntry), null);

        /// <summary>
        /// Filter Text
        /// </summary>
        public static readonly DependencyProperty FilterTextProperty = DependencyProperty.Register("FilterText", typeof(String), typeof(SearchEntry), new UIPropertyMetadata(String.Empty));

        /// <summary>
        /// Gets or sets the access key text.  This is use to assign a hot key to set focus to the search text box.  Example:  _A is a hot key for ALT + A
        /// </summary>
        /// <value>The access key text.</value>
        [Category(_CATEGORY_NAME)]
        [Description("Gets or sets the access key text.  This is use to assign a hot key to set focus to the search text box.  Example:  _A is a hot key for ALT + A")]
        public String AccessKeyText {
            get { return (String)GetValue(AccessKeyTextProperty); }
            set { SetValue(AccessKeyTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the filter border style.
        /// </summary>
        /// <value>The filter border style.</value>
        [Category(_CATEGORY_NAME)]
        [Description("Gets or sets the filter border style.")]
        public Style FilterBorderStyle {
            get { return (Style)GetValue(FilterBorderStyleProperty); }
            set { SetValue(FilterBorderStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the filter reset button style.
        /// </summary>
        /// <value>The filter reset button style.</value>
        [Category(_CATEGORY_NAME)]
        [Description("Gets or sets the filter reset button style.")]
        public Style FilterResetButtonStyle {
            get { return (Style)GetValue(FilterResetButtonStyleProperty); }
            set { SetValue(FilterResetButtonStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the filter text.  This is the search text the user entered.
        /// </summary>
        /// <value>The filter text.</value>
        [Category(_CATEGORY_NAME)]
        [Description("Gets or sets the filter text.  This is the search text the user entered")]
        public String FilterText {
            get { return (String)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        /// <summary>
        /// Gets or sets the filter text box style.
        /// </summary>
        /// <value>The filter text box style.</value>
        [Category(_CATEGORY_NAME)]
        [Description("Gets or sets the filter text box style.")]
        public Style FilterTextBoxStyle {
            get { return (Style)GetValue(FilterTextBoxStyleProperty); }
            set { SetValue(FilterTextBoxStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the filter text box watermark text.  This text is diplayed until the user enters the first character.
        /// </summary>
        /// <value>The filter text box water mark text.</value>
        [Category(_CATEGORY_NAME)]
        [Description("Gets or sets the filter text box watermark text.  This text is diplayed until the user enters the first character.")]
        public String FilterTextBoxWaterMarkText {
            get { return (String)GetValue(FilterTextBoxWaterMarkTextProperty); }
            set { SetValue(FilterTextBoxWaterMarkTextProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchEntry"/> class.
        /// </summary>
        public SearchEntry() { }

        /// <summary>
        /// Initializes the <see cref="SearchEntry"/> class.
        /// </summary>
        static SearchEntry() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SearchEntry), new FrameworkPropertyMetadata(typeof(SearchEntry)));
        }

        #endregion

        #region Methods

        void _filterResetButton_Click(object sender, RoutedEventArgs e) {
            e.Handled = true;
            this.FilterText = String.Empty;
            SearchTextBoxSetFocus();
        }

        void _filterTextBox_KeyDown(object sender, KeyEventArgs e) {

            if (e.Key == Key.Escape) {
                e.Handled = true;
                this.FilterText = String.Empty;
            }
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            _filterResetButton = (Button)GetTemplateChild(_FILTERRESETBUTTON_NAME);
            if(_filterResetButton == null) {
                throw new NullReferenceException(_FILTERRESETBUTTON_NAME);
            }
            _filterResetButton.Click += _filterResetButton_Click;
           
            _filterTextBox = (TextBox)GetTemplateChild(_FILTERTEXTBOX_NAME);
            if(_filterTextBox == null) {
                throw new NullReferenceException(_FILTERTEXTBOX_NAME);
            }
            _filterTextBox.KeyDown += _filterTextBox_KeyDown;

            SearchTextBoxSetFocus();
        }

        /// <summary>
        /// Invoked whenever an unhandled <see cref="E:System.Windows.UIElement.GotFocus"/> event reaches this element in its route.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.RoutedEventArgs"/> that contains the event data.</param>
        protected override void OnGotFocus(RoutedEventArgs e)   {
            SearchTextBoxSetFocus();
        }

        /// <summary>
        /// Sets focus to the Search TextBox.
        /// </summary>
        public void SearchTextBoxSetFocus() {

            if (_filterTextBox != null)
                _filterTextBox.Focus();
        }

        #endregion
    }
}
