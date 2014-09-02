using System;
using System.Windows;
using Microsoft.Practices.Unity;

namespace CookMe {

    public partial class ShellView : Window {

        [Dependency]
        public ShellViewModel ShellViewModel {
            get { return this.DataContext as ShellViewModel; }
            set { this.DataContext = value; }
        }
        
        public ShellView() {
            InitializeComponent();
        }

        void ExitButton_Click(Object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
