
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Ocean.Wpf.Controls {
    /// <summary>
    /// Represents CheckListBox
    /// </summary>
    [TemplatePart(Name = "PART_IndicatorList", Type = typeof(ItemsControl))]
    public class CheckListBox : ContentControl {

        #region Declarations

        ItemsControl _indicatorList;
        ObservableCollection<CheckListBoxIndicatorItem> _indicatorOffsets;
        ListBox _listBox;

        #endregion

        #region  Public Declarations

        /// <summary>
        /// Check glyph brush
        /// </summary>
        public static readonly DependencyProperty CheckBrushProperty = DependencyProperty.Register("CheckBrush", typeof(Brush), typeof(CheckListBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Check glyph border thicknes
        /// </summary>
        public static readonly DependencyProperty CheckBrushStrokeThicknessProperty = DependencyProperty.Register("CheckBrushStrokeThickness", typeof(Double), typeof(CheckListBox), new PropertyMetadata(2.0));

        /// <summary>
        /// Check glyph  height and width
        /// </summary>
        public static readonly DependencyProperty CheckHeightWidthProperty = DependencyProperty.Register("CheckHeightWidth", typeof(Double), typeof(CheckListBox), new PropertyMetadata(13.0));

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the check brush.
        /// </summary>
        /// <value>The check brush.</value>
        [Description("Brush used to paint the check inside the checkbox.  Defaults to black."), Category("Custom")]
        public Brush CheckBrush {
            get {
                return (Brush)(GetValue(CheckBrushProperty));
            }
            set {
                SetValue(CheckBrushProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the check brush stroke thickness.
        /// </summary>
        /// <value>The check brush stroke thickness.</value>
        [Description("Stroke thickness for the check inside the checkbox.  Defaults to 2."), Category("Custom")]
        public double CheckBrushStrokeThickness {
            get {
                return Convert.ToDouble(GetValue(CheckBrushStrokeThicknessProperty));
            }
            set {
                SetValue(CheckBrushStrokeThicknessProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the check height.
        /// </summary>
        /// <value>The width of the check height.</value>
        [Description("Size of CheckBox.  CheckBox is rendered in a square so this value is the height and width of the CheckBox.  Default value is 13."), Category("Custom")]
        public double CheckHeightWidth {
            get {
                return Convert.ToDouble(GetValue(CheckHeightWidthProperty));
            }
            set {
                SetValue(CheckHeightWidthProperty, value);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckListBox"/> class.
        /// </summary>
        public CheckListBox() {
            this.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(CheckBox_Clicked));
            this.SizeChanged += CheckListBox_SizeChanged;
            this.Loaded += ListBoxSelectedItemIndicator_Loaded;
            this.Unloaded += ListBoxSelectedItemIndicator_Unloaded;
        }

        /// <summary>
        /// Initializes the <see cref="CheckListBox"/> class.
        /// </summary>
        static CheckListBox() {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CheckListBox), new FrameworkPropertyMetadata(typeof(CheckListBox)));
        }

        #endregion

        #region  Methods
        
        void CheckBox_Clicked(object sender, RoutedEventArgs e) {

            var cb = e.OriginalSource as CheckBox;

            if(cb == null) {
                return;
            }

            var checkListBoxIndicatorItem = cb.DataContext as CheckListBoxIndicatorItem;

            if(checkListBoxIndicatorItem == null) {
                return;
            }

            checkListBoxIndicatorItem.RelatedListBoxItem.IsSelected = Convert.ToBoolean(cb.IsChecked);
        }

        void CheckListBox_SizeChanged(Object sender, SizeChangedEventArgs e) {
            UpdateIndicators();
        }

        void ListBox_ScrollViewer_ScrollChanged(Object sender, ScrollChangedEventArgs e) {

            //if the user is scrolling horizontality, no reason to run any of our attached behavior code
            if(e.VerticalChange == 0) {
                return;
            }

            UpdateIndicators();
        }

        void ListBox_SelectionChanged(Object sender, SelectionChangedEventArgs e) {
            UpdateIndicators();
        }

        void ListBoxSelectedItemIndicator_Loaded(Object sender, RoutedEventArgs e) {

            if(_indicatorList == null) {
                //remember how much "fun" tabs were be in VB and Access?  Well...
                //
                //this is here because when you place a custom control in a tab, it loads the control once before it runs OnApplyTemplate
                //when the TabItem its in gets clicked (focus), OnApplyTemplate runs then Loaded runs agin.
                return;
            }

            _indicatorOffsets = new ObservableCollection<CheckListBoxIndicatorItem>();
            _indicatorList.ItemsSource = _indicatorOffsets;
            //How cool are routed events!  We can listen into the child ListBoxes activities and act accordingly.
            this.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(ListBox_SelectionChanged));
            this.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(ListBox_ScrollViewer_ScrollChanged));
            UpdateIndicators();
        }

        void ListBoxSelectedItemIndicator_Unloaded(Object sender, RoutedEventArgs e) {
            this.RemoveHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(ListBox_SelectionChanged));
            this.RemoveHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(ListBox_ScrollViewer_ScrollChanged));
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate"/>.
        /// </summary>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            //when the template is applied, this give the developer the oppurtunity to get references to name objects in the control template.
            //in our case, we need a reference to the ItemsControl that holds the indicator arrows.
            //
            //what your control does in the absence of an expected object in the control template is up to the control develeper.
            //in my case here, without the items control, we are dead in the water.
            //
            //remember that custom controls are supposed to be Lookless.  Meaning the visual and code are highly decoupled.  
            //Any designer using Blend fully expects to be able edit the control template anyway they want.
            //My using the "PART_" naming convention, you indicate that this object is probably necessary for the conrol to work, but this is not true in all cases.
            //
            _indicatorList = GetTemplateChild("PART_IndicatorList") as ItemsControl;

            if(_indicatorList == null) {
                throw new Exception(Properties.Resources.CheckListBox_OnApplyTemplate_Hey__The_PART_IndicatorList_is_missing_from_the_template_or_is_not_an_ItemsControl___Sorry_but_this_ItemsControl_is_required_);
            }
        }

        /// <summary>
        /// Called when the <see cref="P:System.Windows.Controls.ContentControl.Content"/> property changes.
        /// </summary>
        /// <param name="oldContent">The old value of the <see cref="P:System.Windows.Controls.ContentControl.Content"/> property.</param>
        /// <param name="newContent">The new value of the <see cref="P:System.Windows.Controls.ContentControl.Content"/> property.</param>
        protected override void OnContentChanged(Object oldContent, Object newContent) {
            base.OnContentChanged(oldContent, newContent);

            //this is our insurance policy that the developer does not add content that is not a ListBox
            if(newContent == null || newContent is ListBox) {
                //this ensures that our reference to the child ListBox is always correct or nothing.
                //if the child ListBox is removed, our reference is set to Nothing
                //if the child ListBox is swapped out, our reference is set to the newContent
                _listBox = this.Content as ListBox;

                //this removes our references to the ListBox items
                if(_indicatorOffsets != null && _indicatorOffsets.Count > 0) {
                    _indicatorOffsets.Clear();
                }

                return;

            }
            throw new NotSupportedException(Properties.Resources.CheckListBox_OnContentChanged_Invalid_content__CheckListBox_only_accepts_a_ListBox_control_as_its_content_);
        }

        void UpdateIndicators() {

            //This is the awesome procedure that Josh Smith authored with a few modifications
            if(_indicatorOffsets == null) {
                return;
            }

            if(_listBox == null) {
                return;
            }

            if(_indicatorOffsets.Count > 0) {
                _indicatorOffsets.Clear();
            }

            ItemContainerGenerator gen = _listBox.ItemContainerGenerator;

            if(gen.Status != GeneratorStatus.ContainersGenerated) {
                return;
            }

            foreach(Object selectedItem in _listBox.Items) {

                var lbi = gen.ContainerFromItem(selectedItem) as ListBoxItem;

                if(lbi == null) {
                    continue;
                }

                GeneralTransform transform = lbi.TransformToAncestor(_listBox);
                Point pt = transform.Transform(new Point(0, 0));
                Double offset = pt.Y + (lbi.ActualHeight / 2) - (CheckHeightWidth / 2);
                _indicatorOffsets.Add(new CheckListBoxIndicatorItem(offset, lbi.IsSelected, lbi));
            }
        }

        #endregion
    }
}