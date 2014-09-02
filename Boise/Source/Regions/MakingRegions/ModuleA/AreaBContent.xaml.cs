using System.Windows.Controls;

namespace ModuleA
{
    /// <summary>
    /// Interaction logic for AreaBContent.xaml
    /// </summary>
    public partial class AreaBContent : UserControl
    {
        public AreaBContent()
        {
            InitializeComponent();
        }

        public string ViewName { get { return "AreaB"; } }
    }
}
