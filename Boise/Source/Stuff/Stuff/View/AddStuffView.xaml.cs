
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Simple.WPF.Infrastructure;
using Stuff.BusinessEntityObjects;
using Stuff.ViewModel;

namespace Stuff.View {

    /// <summary>
    /// Represents an AddStuffView
    /// </summary>
    public partial class AddStuffView : UserControl {

        #region Declarations

        AddStuffViewModel _vm;
        Storyboard _waitingStoryboard;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddStuffView"/> class.
        /// </summary>
        public AddStuffView() {
            InitializeComponent();
            _waitingStoryboard = this.FindResource("sbWaiting") as Storyboard;
        }

        #endregion

        #region Methods

        //The  DockPanel_Button_Click method works because the of the way the XAML was written for the Image control.
        //Notice the soure is explicitly set to a BitmapImage.
        //
        //By writing the application this way, we avoid handling the download ourselves.  The below method simply
        //extracts information we have already downloaded.
        //
        //UI XAML:
        //
        //  <Image VerticalAlignment="Top"  DockPanel.Dock="Left" Height="90" Width="65" Margin="7" >
        //      <Image.Source>
        //          <BitmapImage UriSource="{Binding Path=ImageURL}" />
        //      </Image.Source>
        //  </Image>
        //
        void DockPanel_Button_Click(Object sender, RoutedEventArgs e) {

            DockPanel dock = sender as DockPanel;

            if (dock != null) {

                if (_vm == null)
                    _vm = (AddStuffViewModel)DataContext;
                Movie movie = (MovieSearchResult)dock.DataContext;
                var image = Helper.FindVisualChild<Image>(dock);

                using (MemoryStream memoryStream = new MemoryStream()) {

                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(image.Source as BitmapImage));
                    encoder.Save(memoryStream);
                    movie.Image = memoryStream.GetBuffer();
                }
            }
            e.Handled = true;
        }

        void ellipseWaiting_IsVisibleChanged(Object sender, DependencyPropertyChangedEventArgs e) {

            if (_waitingStoryboard != null && e.NewValue != null && (Boolean)e.NewValue == true) {
                _waitingStoryboard.Begin(this);
            } else if (_waitingStoryboard != null) {
                _waitingStoryboard.Stop(this);
            }
        }

        void ListBox_SelectionChanged(Object sender, SelectionChangedEventArgs e) {
            ListBox lb = e.OriginalSource as ListBox;

            if (lb != null && lb.SelectedIndex != -1 && e.AddedItems != null && e.AddedItems[0] != null) {
                ListBoxItem lbi = lb.ItemContainerGenerator.ContainerFromItem(e.AddedItems[0]) as ListBoxItem;
                TextBox tb = Helper.FindVisualChild<TextBox>(lbi);

                if (tb != null)
                    tb.Focus();
            }
        }

        #endregion
    }
}
