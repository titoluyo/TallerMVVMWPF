using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace ThePhoneCompany.Information.Views {

    [Export(typeof(HomeView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class HomeView : UserControl {
        public HomeView() {
            InitializeComponent();
        }
    }
}
