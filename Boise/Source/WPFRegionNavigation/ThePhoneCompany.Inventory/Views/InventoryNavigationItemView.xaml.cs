using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using ThePhoneCompany.Common.Constants;

namespace ThePhoneCompany.Inventory.Views {

    [Export]
    [ViewSortHint(Constants.InventorySortHint)]
    public partial class InventoryNavigationItemView : UserControl {

        [Import]
        public InventoryNavigationItemViewModel ViewModel {
            get { return this.DataContext as InventoryNavigationItemViewModel; }
            set { this.DataContext = value; }
        }

        public InventoryNavigationItemView() {
            InitializeComponent();
        }

    }
}
