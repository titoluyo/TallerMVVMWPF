using System.Windows.Controls;
using Microsoft.Practices.Unity;

namespace CookMe.Recipe.Views {

    public partial class SearchView : UserControl {

        [Dependency]
        public SearchViewModel SearchViewModel {
            get { return this.DataContext as SearchViewModel; }
            set {this.DataContext = value;}
        }

        public SearchView() {
            InitializeComponent();
        }
    }
}
