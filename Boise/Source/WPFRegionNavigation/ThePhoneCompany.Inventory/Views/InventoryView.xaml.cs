using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ThePhoneCompany.Inventory.Views {

    [Export(typeof(InventoryView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class InventoryView : UserControl {
        
        public InventoryView() {
            InitializeComponent();
        }

        [Import]
        public InventoryViewModel ViewModel {
            get { return this.DataContext as InventoryViewModel; }
            set { this.DataContext = value; }
        }
    }
}
