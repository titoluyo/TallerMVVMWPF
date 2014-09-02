using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;

namespace ModuleA
{
    /// <summary>
    /// Interaction logic for AreaAContent.xaml
    /// </summary>
    public partial class AreaAContent : UserControl
    {
        private readonly IRegionManager _regionManager;

        public AreaAContent(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeComponent();

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e) =>
                                        {
                                            object value =
                                                RegionContext.GetObservableContext(
                                                    this).Value;

                                            ContextValue.Text = value != null
                                                                    ? value.ToString
                                                                            ()
                                                                    : "Not Set!";
                                        };
        }

        public string ViewName
        {
            get { return "AreaA"; }
        }
    }
}