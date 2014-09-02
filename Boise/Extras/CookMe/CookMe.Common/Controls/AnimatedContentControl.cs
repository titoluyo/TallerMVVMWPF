using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CookMe.Common.Controls {
    /// <summary>
    /// A ContentControl that animates the transition between content
    /// Learned a lot from this CodeProject article, made only minor enhancements. 
    /// http://www.codeproject.com/KB/WPF/AnimatedContentControl.aspx
    /// </summary>
    [TemplatePart(Name = "PART_PaintArea", Type = typeof(Shape))]
    [TemplatePart(Name = "PART_MainContent", Type = typeof(ContentPresenter))]
    public class AnimatedContentControl : ContentControl {

        #region Properties

        public TransitionType TransitionType {
            get { return (TransitionType)GetValue(TransitionTypeProperty); }
            set { SetValue(TransitionTypeProperty, value); }
        }

        public static readonly DependencyProperty TransitionTypeProperty =
            DependencyProperty.Register("TransitionType", typeof(TransitionType), typeof(AnimatedContentControl), new PropertyMetadata(TransitionType.Slide));

        public Int32 Duration {
            get { return (Int32)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(Int32), typeof(AnimatedContentControl), new PropertyMetadata(500));

        #endregion // Properties

        #region Generated static constructor

        static AnimatedContentControl() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnimatedContentControl), new FrameworkPropertyMetadata(typeof(AnimatedContentControl)));
        }

        #endregion //Generated static constructor

        #region Declarations

        Shape _paintArea;
        ContentPresenter _mainContent;

        #endregion //Declarations

        #region Methods

        /// <summary>
        /// This gets called when the template has been applied and we have our visual tree
        /// </summary>
        public override void OnApplyTemplate() {
            _paintArea = Template.FindName("PART_PaintArea", this) as Shape;
            _mainContent = Template.FindName("PART_MainContent", this) as ContentPresenter;

            base.OnApplyTemplate();
        }

        /// <summary>
        /// This gets called when the content we're displaying has changed
        /// </summary>
        /// <param name="oldContent">The content that was previously displayed</param>
        /// <param name="newContent">The new content that is displayed</param>
        protected override void OnContentChanged(object oldContent, object newContent) {
            if (_paintArea != null && _mainContent != null) {
                _paintArea.Fill = CreateBrushFromVisual(_mainContent);
                switch (this.TransitionType) {
                    case TransitionType.Slide:
                        BeginAnimateContentReplacement();
                        break;
                    case TransitionType.Fade:
                        FadeTransition();
                        break;
                    default:
                        break;
                }
            }
            base.OnContentChanged(oldContent, newContent);
        }

        void FadeTransition() {
            _paintArea.Visibility = Visibility.Visible;
            _paintArea.Opacity = 1;
            _paintArea.InvalidateVisual();
            _paintArea.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, new Duration(TimeSpan.FromMilliseconds(this.Duration))));
            var animation = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(this.Duration)));
            animation.Completed += (s, e) => _paintArea.Visibility = Visibility.Hidden;
            _mainContent.BeginAnimation(OpacityProperty, animation);
        }

        /// <summary>
        /// Starts the animation for the new content
        /// </summary>
        void BeginAnimateContentReplacement() {
            var newContentTransform = new TranslateTransform();
            var oldContentTransform = new TranslateTransform();
            _paintArea.RenderTransform = oldContentTransform;
            _mainContent.RenderTransform = newContentTransform;
            _paintArea.Visibility = Visibility.Visible;

            newContentTransform.BeginAnimation(TranslateTransform.XProperty, CreateAnimation(this.ActualWidth, 0));
            oldContentTransform.BeginAnimation(TranslateTransform.XProperty, CreateAnimation(0, -this.ActualWidth, (s, e) => _paintArea.Visibility = Visibility.Hidden));
        }

        /// <summary>
        /// Creates the animation that moves content in or out of view.
        /// </summary>
        /// <param name="from">The starting value of the animation.</param>
        /// <param name="to">The end value of the animation.</param>
        /// <param name="whenDone">(optional) A callback that will be called when the animation has completed.</param>
        AnimationTimeline CreateAnimation(Double from, Double to, EventHandler whenDone = null) {
            IEasingFunction ease = new BackEase { Amplitude = 0.3, EasingMode = EasingMode.EaseOut };
            var duration = new Duration(TimeSpan.FromMilliseconds(this.Duration));
            var anim = new DoubleAnimation(from, to, duration) { EasingFunction = ease };
            if (whenDone != null) {
                anim.Completed += whenDone;
            }
            anim.Freeze();
            return anim;
        }

        /// <summary>
        /// Creates a brush based on the current appearnace of a visual element. The brush is an ImageBrush and once created, won't update its look
        /// </summary>
        /// <param name="visual">The visual element to take a snapshot of</param>
        Brush CreateBrushFromVisual(Visual visual) {
            if (visual == null) {
                throw new ArgumentNullException("visual");
            }
            var target = new RenderTargetBitmap((int)this.ActualWidth, (int)this.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            target.Render(visual);
            var brush = new ImageBrush(target);
            brush.Freeze();
            return brush;
        }

        #endregion //Methods
    }
}
