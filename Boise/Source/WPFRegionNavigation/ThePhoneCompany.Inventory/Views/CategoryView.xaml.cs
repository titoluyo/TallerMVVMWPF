using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ThePhoneCompany.Inventory.Views {

    [Export(typeof(CategoryView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CategoryView : UserControl {
        
        public CategoryView() {
            InitializeComponent();
        }

        [Import]
        public CategoryViewModel ViewModel {
            get { return this.DataContext as CategoryViewModel; }
            set { this.DataContext = value; }
        }
    }
}
