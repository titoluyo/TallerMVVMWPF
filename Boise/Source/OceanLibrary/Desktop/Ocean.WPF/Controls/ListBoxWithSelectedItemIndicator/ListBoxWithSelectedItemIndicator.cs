
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Ocean.Wpf.Controls {
    /// <summary>
    /// Represents ListBoxWithSelectedItemIndicator
    /// </summary>
    [TemplatePart(Name = "PART_IndicatorList", Type = typeof(ItemsControl))]
    public class ListBoxWithSelectedItemIndicator : ContentControl {

        #region Declarations

        ItemsControl _indicatorList;
        ObservableCollection<double> _indicatorOffsets;
        ListBox _listBox;

        #endregion

        #region  Public Declarations

        /// <summary>
        /// Indicator Brush
        /// </summary>
        public static readonly DependencyProperty IndicatorBrushProperty = DependencyProperty.Register("IndicatorBrush", typeof(Brush), typeof(ListBoxWithSelectedItemIndicator), new PropertyMetadata(new LinearGradientBrush(Colors.LightBlue, Colors.Blue, new Point(0.5, 0), new Point(0.5, 1))));

        /// <summary>
        /// Indicator Height Width
        /// </summary>
        public static readonly DependencyProperty IndicatorHeightWidthProperty = DependencyProperty.Register("IndicatorHeightWidth", typeof(Double), typeof(ListBoxWithSelectedItemIndicator), new PropertyMetadata(16.0));

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the indicator brush.
        /// </summary>
        /// <value>The indicator brush.</value>
        [Description("Brush used to paint the indicator.  Defaults to a nice blue gradient brush"), Category("Custom")]
        public Brush IndicatorBrush {
            get {
                return (Brush)(GetValue(IndicatorBrushProperty));
            }
            set {
                SetValue(IndicatorBrushProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the indicator height.
        /// </summary>
        /// <value>The width of the indicator height.</value>
        [Description("Size of indictor.  Indicator is rendered in a square so this value is the height and width of the indicator.  Default value is 16."), Category("Custom")]
        public Double IndicatorHeightWidth {
            get {
                return Convert.ToDouble(GetValue(IndicatorHeightWidthProperty));
            }
            set {
                SetValue(IndicatorHeightWidthProperty, value);
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListBoxWithSelectedItemIndicator"/> class.
        /// </summary>
        public ListBoxWithSelectedItemIndicator() {
            this.Loaded += ListBoxWithSelectedItemIndicator_Loaded;
            this.Unloaded += ListBoxWithSelectedItemIndicator_Unloaded;
        }

        /// <summary>
        /// Initializes the <see cref="ListBoxWithSelectedItemIndicator"/> class.
        /// </summary>
        static ListBoxWithSelectedItemIndicator() {
            //This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
            //This style is defined in themes\generic.xaml
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxWithSelectedItemIndicator), new FrameworkPropertyMetadata(typeof(ListBoxWithSelectedItemIndicator)));
        }

        #endregion

        #region  Methods

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

        void ListBoxWithSelectedItemIndicator_Loaded(Object sender, RoutedEventArgs e) {

            if(_indicatorList == null) {
                //remember how much "fun" tabs were be in VB and Access?  Well...
                //
                //this is here because when you place a custom control in a tab, it loads the control once before it runs OnApplyTemplate
                //when the TabItem its in gets clicked (focus), OnApplyTemplate runs then Loaded runs agin.
                //
                //since OnApplyTemplate has not run yet, we are out of here
                return;
            }

            _indicatorOffsets = new ObservableCollection<double>();
            _indicatorList.ItemsSource = _indicatorOffsets;
            //How cool are routed events!  We can listen into the child ListBoxes activities and act accordingly.
            this.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(ListBox_SelectionChanged));
            this.AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(ListBox_ScrollViewer_ScrollChanged));
            UpdateIndicators();
        }

        void ListBoxWithSelectedItemIndicator_Unloaded(object sender, RoutedEventArgs e) {
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
                throw new Exception(Properties.Resources.ListBoxWithSelectedItemIndicator_OnApplyTemplate_Hey__The_PART_IndicatorList_is_missing_from_the_template_or_is_not_an_ItemsControl___Sorry_but_this_ItemsControl_is_required_);
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
                _listBox = newContent as ListBox;

                //this removes our references to the ListBox items
                if(_indicatorOffsets != null && _indicatorOffsets.Count > 0) {
                    _indicatorOffsets.Clear();
                }

                return;

            }
            throw new NotSupportedException(Properties.Resources.ListBoxWithSelectedItemIndicator_OnContentChanged_Invalid_content__ListBoxWithSelectedItemIndicator_only_accepts_a_ListBox_control_as_its_content_);
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

            if(_listBox.SelectedItems.Count == 0) {
                return;
            }

            ItemContainerGenerator gen = _listBox.ItemContainerGenerator;

            if(gen.Status != GeneratorStatus.ContainersGenerated) {
                return;
            }

            foreach(object objSelectedItem in _listBox.SelectedItems) {

                var lbi = gen.ContainerFromItem(objSelectedItem) as ListBoxItem;

                if(lbi == null) {
                    continue;
                }

                GeneralTransform transform = lbi.TransformToAncestor(_listBox);
                Point pt = transform.Transform(new Point(0, 0));
                Double dblOffset = pt.Y + (lbi.ActualHeight / 2) - (this.IndicatorHeightWidth / 2);
                _indicatorOffsets.Add(dblOffset);
            }
        }

        #endregion
    }
}

