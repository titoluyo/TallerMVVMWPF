
using System;
using System.Windows;
using System.Windows.Controls;

namespace Stuff.View {

    public partial class BrowseStuffView : UserControl {
        public BrowseStuffView() {
            InitializeComponent();
        }

        void MovieCoverViewRadioButton_Checked(Object sender, System.Windows.RoutedEventArgs e) {
            this.itemsBrowser.ItemTemplate = layoutRoot.FindResource("movieCoverImageDataTemplate") as DataTemplate;
            this.movieCoverDetailsForm.Visibility = Visibility.Visible;
        }

        void OutlookCardViewRadioButton_Checked(Object sender, System.Windows.RoutedEventArgs e) {
            this.itemsBrowser.ItemTemplate = layoutRoot.FindResource("outlookCardDataTemplate") as DataTemplate;
            this.movieCoverDetailsForm.Visibility = Visibility.Collapsed;
        }
    }
}
