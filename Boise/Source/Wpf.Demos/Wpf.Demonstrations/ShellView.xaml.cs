using System.Windows;
using Microsoft.Practices.Unity;

namespace Wpf.Demonstrations {

    public partial class ShellView : Window {

        [Dependency]
        public ShellViewModel ShellViewModel {
            get { return this.DataContext as ShellViewModel; }
            set { this.DataContext = value; }
        }

        public ShellView() {
            InitializeComponent();
        }
    }
}
