using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ocean.Wpf.Controls {

    /// <summary>
    /// Represents a Rating control
    /// </summary>
    public class Rating : Control {

        #region Declarations

        const String _CATEGORY_NAME_BRUSHES = "Brushes";
        const String _CATEGORY_NAME_CUSTOM = "Custom";
        const Double _MAXIMUM_VALUE = 5.0;
        const Double _STAR_WIDTH = 50.0;
        Double _saveValue;

        #endregion

        #region Dependency Properties

        /// <summary>
        /// Empty Star Brush
        /// </summary>
        public static readonly DependencyProperty EmptyStarBrushProperty =
            DependencyProperty.Register("EmptyStarBrush", typeof(Brush), typeof(Rating), new PropertyMetadata(new SolidColorBrush(Colors.WhiteSmoke)));

        /// <summary>
        /// Is ReadOnly 
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(Boolean), typeof(Rating), new PropertyMetadata(false));

        /// <summary>
        /// Value Brush
        /// </summary>
        public static readonly DependencyProperty ValueBrushProperty =
            DependencyProperty.Register("ValueBrush", typeof(Brush), typeof(Rating), new PropertyMetadata(new SolidColorBrush(Colors.Blue)));

        /// <summary>
        /// Value Precision
        /// </summary>
        public static readonly DependencyProperty ValuePrecisionProperty =
            DependencyProperty.Register("ValuePrecision", typeof(Int32), typeof(Rating),
            new PropertyMetadata(1, null), ValuePrecisionValidateCallback);

        /// <summary>
        /// Value
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Double), typeof(Rating),
            new PropertyMetadata(0.0, OnValueChanged), ValueValidateCallback);

        /// <summary>
        /// Value Selector Background
        /// </summary>
        public static readonly DependencyProperty ValueSelectorBackgroundProperty =
            DependencyProperty.Register("ValueSelectorBackground", typeof(Brush), typeof(Rating), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        /// <summary>
        /// Value Width
        /// </summary>
        internal static readonly DependencyProperty ValueWidthProperty =
            DependencyProperty.Register("ValueWidth", typeof(Double), typeof(Rating), new PropertyMetadata(0.0));
        
        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var r = d as Rating;

            if (r != null) {
                r.ValueWidth = ((Double)e.NewValue) * _STAR_WIDTH;
            }
        }
        static Boolean ValuePrecisionValidateCallback(Object value) {
            var v = (Int32)value;
            return v >= 0;
        }
        static Boolean ValueValidateCallback(Object value) {

            var v = (Double)value;
            return v >= 0 && v <= _MAXIMUM_VALUE;
        }

        /// <summary>
        /// Gets or sets the brush used to paint empty stars.
        /// </summary>
        /// <value>The empty star brush.</value>
        [Category(_CATEGORY_NAME_BRUSHES)]
        [Description("Gets or sets the brush used to paint empty stars")]
        public Brush EmptyStarBrush {
            get { return (Brush)GetValue(EmptyStarBrushProperty); }
            set { SetValue(EmptyStarBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        [Category(_CATEGORY_NAME_CUSTOM)]
        [Description("Gets or sets a value indicating whether this instance is read only")]
        public Boolean IsReadOnly {
            get { return (Boolean)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Category(_CATEGORY_NAME_CUSTOM)]
        [Description("Gets or sets the value")]
        public Double Value {
            get { return (Double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value brush.  This brush is used to paint the 5 stars as the value increases.
        /// </summary>
        /// <value>The value brush.</value>
        [Category(_CATEGORY_NAME_BRUSHES)]
        [Description("Gets or sets the value brush.  This brush is used to paint the 5 stars as the value increases")]
        public Brush ValueBrush {
            get { return (Brush)GetValue(ValueBrushProperty); }
            set { SetValue(ValueBrushProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value precision or how many digits to round the value to.  
        /// </summary>
        /// <value>The value precision.</value>
        [Category(_CATEGORY_NAME_CUSTOM)]
        [Description("Gets or sets the value precision or how many digits to round the value to")]
        public Int32 ValuePrecision {
            get { return (Int32)GetValue(ValuePrecisionProperty); }
            set { SetValue(ValuePrecisionProperty, value); }
        }

        /// <summary>
        /// Gets or sets the value selector background.  The value selector background is the area inside the control but outside the 5 stars.
        /// </summary>
        /// <value>The value selector background.</value>
        [Category(_CATEGORY_NAME_BRUSHES)]
        [Description("Gets or sets the value selector background.  The value selector background is the area inside the control but outside the 5 stars")]
        public Brush ValueSelectorBackground {
            get { return (Brush)GetValue(ValueSelectorBackgroundProperty); }
            set { SetValue(ValueSelectorBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the value rectangle.
        /// </summary>
        /// <value>The width of the value.</value>
        /// <remarks>
        /// This needs to be available for TemplateBinding, but is not visible in the properties window or code editor.
        /// Additionally it has internal scope to limit visibility to the current assembly only.
        /// </remarks>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        internal Double ValueWidth {
            get { return (Double)GetValue(ValueWidthProperty); }
            set { SetValue(ValueWidthProperty, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        public Rating() { }

        /// <summary>
        /// Initializes the <see cref="Rating"/> class.
        /// </summary>
        static Rating() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Rating), new FrameworkPropertyMetadata(typeof(Rating)));
        }

        #endregion

        #region Methods

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            this.ValueWidth = this.Value * _STAR_WIDTH;
        }

        /// <summary>
        /// Invoked when an unhandled MouseEnter attached event is raised on this element. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseEnter(System.Windows.Input.MouseEventArgs e) {
            base.OnMouseEnter(e);

            if (!IsReadOnly)
                _saveValue = this.Value;
        }

        /// <summary>
        /// Invoked when an unhandled MouseLeave attached event is raised on this element. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseLeave(System.Windows.Input.MouseEventArgs e) {
            base.OnMouseLeave(e);

            if (!IsReadOnly)
                Value = _saveValue;
        }

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.UIElement.MouseLeftButtonDown"/> routed event is raised on this element. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseButtonEventArgs"/> that contains the event data. The event data reports that the left mouse button was pressed.</param>
        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e) {
            base.OnMouseLeftButtonDown(e);

            if (!IsReadOnly)
                _saveValue = this.Value;
        }

        /// <summary>
        /// Invoked when an unhandled MouseMove attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.MouseEventArgs"/> that contains the event data.</param>
        protected override void OnMouseMove(System.Windows.Input.MouseEventArgs e) {
            base.OnMouseMove(e);

            if (IsReadOnly) return;
            
            Double ratingWidth = this.ActualWidth;

            Double mouseX = e.GetPosition(this).X;

            if (mouseX == 0) {
                Value = 0;
            } else if (mouseX >= ratingWidth) {
                Value = _MAXIMUM_VALUE;
            } else {
                Value = Math.Round((mouseX / ratingWidth) * _MAXIMUM_VALUE, this.ValuePrecision);
            }
        }

        /// <summary>
        /// Invoked when an unhandled KeyDown attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.KeyEventArgs"/> that contains the event data.</param>
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e) {
            base.OnKeyDown(e);
            String key = e.Key.ToString();

            if (key.StartsWith("D")) {
                e.Handled = SetValueIfWithRange(key.Remove(0, 1));
            } else if (key.StartsWith("NumPad")) {
                e.Handled = SetValueIfWithRange(key.Remove(0, 6));
            }
        }

        Boolean SetValueIfWithRange(String key) {
            Int32 value;
            if (Int32.TryParse(key, out value) && value >= 0 && value <= _MAXIMUM_VALUE) {
                Value = value;
                return true;
            }

            return false;
        }

        #endregion
    }
}
