using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Ocean.Wpf.Controls {

    /// <summary>
    /// Represents CustomToolBarButton
    /// </summary>
    public class CustomToolBarButton : Button {

        #region  Shared Declarations

        /// <summary>
        /// Button layout
        /// </summary>
        public static DependencyProperty ButtonLayoutProperty = DependencyProperty.Register("ButtonLayout", typeof(Orientation), typeof(CustomToolBarButton), new PropertyMetadata(Orientation.Horizontal));

        /// <summary>
        /// Button pressed background
        /// </summary>
        public static DependencyProperty ButtonPressedBackgroundProperty = DependencyProperty.Register("ButtonPressedBackground", typeof(Brush), typeof(CustomToolBarButton));

        /// <summary>
        /// Button pressed border
        /// </summary>
        public static DependencyProperty ButtonPressedBorderProperty = DependencyProperty.Register("ButtonPressedBorder", typeof(Brush), typeof(CustomToolBarButton));

        /// <summary>
        /// Button text
        /// </summary>
        public static DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(CustomToolBarButton));

        /// <summary>
        /// Disabled button image
        /// </summary>
        public static DependencyProperty DisabledButtonImageProperty = DependencyProperty.Register("DisabledButtonImage", typeof(ImageSource), typeof(CustomToolBarButton));

        /// <summary>
        /// Enabled button image
        /// </summary>
        public static DependencyProperty EnabledButtonImageProperty = DependencyProperty.Register("EnabledButtonImage", typeof(ImageSource), typeof(CustomToolBarButton));

        /// <summary>
        /// Mouse over background
        /// </summary>
        public static DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(CustomToolBarButton));

        /// <summary>
        /// Move over boarder
        /// </summary>
        public static DependencyProperty MouseOverBorderProperty = DependencyProperty.Register("MouseOverBorder", typeof(Brush), typeof(CustomToolBarButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Move over foreground
        /// </summary>
        public static DependencyProperty MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(CustomToolBarButton));

        /// <summary>
        /// Show button image
        /// </summary>
        public static DependencyProperty ShowButtonImageProperty = DependencyProperty.Register("ShowButtonImage", typeof(Boolean), typeof(CustomToolBarButton), new PropertyMetadata(true));

        /// <summary>
        /// Show button text
        /// </summary>
        public static DependencyProperty ShowButtonTextProperty = DependencyProperty.Register("ShowButtonText", typeof(Boolean), typeof(CustomToolBarButton), new PropertyMetadata(false));

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the button layout.
        /// </summary>
        /// <value>The button layout.</value>
        [Category("Custom"), Description("This sets the position of the text in relation to the button image.")]
        public Orientation ButtonLayout {
            get {
                return (Orientation)(GetValue(ButtonLayoutProperty));
            }
            set {
                SetValue(ButtonLayoutProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the button pressed background.
        /// </summary>
        /// <value>The button pressed background.</value>
        [Category("Custom"), Description("Button pressed background brush.")]
        public Brush ButtonPressedBackground {
            get {
                return (Brush)(GetValue(ButtonPressedBackgroundProperty));
            }
            set {
                SetValue(ButtonPressedBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the button pressed border.
        /// </summary>
        /// <value>The button pressed border.</value>
        [Category("Custom"), Description("Button pressed border brush.")]
        public Brush ButtonPressedBorder {
            get {
                return (Brush)(GetValue(ButtonPressedBorderProperty));
            }
            set {
                SetValue(ButtonPressedBorderProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the button text.
        /// </summary>
        /// <value>The button text.</value>
        [Category("Custom"), Description("Text for the button.")]
        public String ButtonText {
            get {
                return Convert.ToString(GetValue(ButtonTextProperty));
            }
            set {
                SetValue(ButtonTextProperty, value);
            }
        }

        /// <summary>
        /// I did this since we have derived from button, but don't use the
        /// content property like other buttons.  The control template for
        /// this control is the content of this button.  Doing this, just
        /// prevents this property from showing up in the GUI designers.
        /// </summary>
        /// <value></value>
        /// <returns>An object that contains the control's content. The default value is null.</returns>
        [Browsable(false)]
        public new Object Content {
            get {
                return base.Content;
            }
            set {
                base.Content = value;
            }
        }

        /// <summary>
        /// Gets or sets the disabled button image.
        /// </summary>
        /// <value>The disabled button image.</value>
        [Category("Custom"), Description("Image to display when the button is disabled.")]
        public ImageSource DisabledButtonImage {
            get {
                return (ImageSource)(GetValue(DisabledButtonImageProperty));
            }
            set {
                SetValue(DisabledButtonImageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the enabled button image.
        /// </summary>
        /// <value>The enabled button image.</value>
        [Category("Custom"), Description("Image to display when the button is enabled.")]
        public ImageSource EnabledButtonImage {
            get {
                return (ImageSource)(GetValue(EnabledButtonImageProperty));
            }
            set {
                SetValue(EnabledButtonImageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the mouse over background.
        /// </summary>
        /// <value>The mouse over background.</value>
        [Category("Custom"), Description("Mouse over background brush.")]
        public Brush MouseOverBackground {
            get {
                return (Brush)(GetValue(MouseOverBackgroundProperty));
            }
            set {
                SetValue(MouseOverBackgroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the mouse over border.
        /// </summary>
        /// <value>The mouse over border.</value>
        [Category("Custom"), Description("Mouse over border brush.")]
        public Brush MouseOverBorder {
            get {
                return (Brush)(GetValue(MouseOverBorderProperty));
            }
            set {
                SetValue(MouseOverBorderProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the mouse over foreground.
        /// </summary>
        /// <value>The mouse over foreground.</value>
        [Category("Custom"), Description("Mouse over foreground text brush.")]
        public Brush MouseOverForeground {
            get {
                return (Brush)(GetValue(MouseOverForegroundProperty));
            }
            set {
                SetValue(MouseOverForegroundProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show button image].
        /// </summary>
        /// <value><c>true</c> if [show button image]; otherwise, <c>false</c>.</value>
        [Category("Custom"), Description("Display the image on the button.")]
        public Boolean ShowButtonImage {
            get {
                return Convert.ToBoolean(GetValue(ShowButtonImageProperty));
            }
            set {
                SetValue(ShowButtonImageProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show button text].
        /// </summary>
        /// <value><c>true</c> if [show button text]; otherwise, <c>false</c>.</value>
        [Category("Custom"), Description("Display the text on the button.")]
        public Boolean ShowButtonText {
            get {
                return Convert.ToBoolean(GetValue(ShowButtonTextProperty));
            }
            set {
                SetValue(ShowButtonTextProperty, value);
            }
        }

        #endregion

        #region  Constructor

        /// <summary>
        /// Initializes the <see cref="CustomToolBarButton"/> class.
        /// </summary>
        static CustomToolBarButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomToolBarButton), new FrameworkPropertyMetadata(typeof(CustomToolBarButton)));
        }

        #endregion
    }
}