using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Ocean.Wpf.Controls {

    /// <summary> 
    /// Renders a TextBlock in an element's adorner layer. 
    /// </summary> 
    /// <remarks>
    /// Thanks for Josh Smith for this great code:
    /// http://joshsmithonwpf.wordpress.com/2007/08/25/rendering-text-in-the-adorner-layer/
    /// </remarks>
    public class TextBlockAdorner : Adorner {

        #region  Declarations

        ArrayList _logicalChildren;
        readonly TextBlock _textBlock;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TextBlockAdorner"/> class.
        /// </summary>
        /// <param name="adornedElement">The adorned element.</param>
        /// <param name="textBlock">The text block.</param>
        public TextBlockAdorner(UIElement adornedElement, TextBlock textBlock)
            : base(adornedElement) {
            _textBlock = textBlock;
            // Register the TextBlock with the element tree so that 
            // it will be rendered, and can inherit DP values. 
            this.AddLogicalChild(_textBlock);
            this.AddVisualChild(_textBlock);
        }

        #endregion

        #region Measure/Arrange

        /// <summary> 
        /// Positions and sizes the TextBlock. 
        /// </summary> 
        /// <param name="finalSize">The actual size of the TextBlock.</param> 
        protected override Size ArrangeOverride(Size finalSize) {

            var location = new Point(0, 0);
            var rect = new Rect(location, finalSize);
            _textBlock.Arrange(rect);
            return finalSize;
        }

        /// <summary> 
        /// Allows the TextBlock to determine how big it wants to be. 
        /// </summary> 
        /// <param name="constraint">A limiting size for the TextBlock.</param> 
        protected override Size MeasureOverride(Size constraint) {
            _textBlock.Measure(constraint);
            return _textBlock.DesiredSize;
        }

        #endregion

        #region Visual Children

        /// <summary> 
        /// Required for the TextBlock to be rendered. 
        /// </summary> 
        protected override Visual GetVisualChild(Int32 index) {

            if(index != 0) {
                throw new ArgumentOutOfRangeException("index");
            }

            return _textBlock;
        }

        /// <summary> 
        /// Required for the TextBlock to be rendered. 
        /// </summary> 
        protected override Int32 VisualChildrenCount {
            get {
                return 1;
            }
        }

        #endregion

        #region Logical Children

        /// <summary> 
        /// Required for the TextBlock to inherit property values 
        /// from the logical tree, such as FontSize. 
        /// </summary> 
        protected override IEnumerator LogicalChildren {
            get {

                if(_logicalChildren == null) {
                    _logicalChildren = new ArrayList {_textBlock};
                }

                return _logicalChildren.GetEnumerator();
            }
        }

        #endregion
    }
}