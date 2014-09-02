using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ThePhoneCompany.Inventory.Views {

    [Export(typeof(ItemView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ItemView : UserControl {
        
        public ItemView() {
            InitializeComponent();
        }

        [Import]
        public ItemViewModel ViewModel {
            get { return this.DataContext as ItemViewModel; }
            set { this.DataContext = value; }
        }
    }
}
