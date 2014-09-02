using System.Windows.Controls;
using Common;
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
            var innerRegion = _regionManager.CreateRegionManager();

            InitializeComponent();
            

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e) =>
                                        {
                                            object value =
                                                RegionContext.GetObservableContext(
                                                    this).Value;

                                            ContextValue.Text = value != null
                                                                    ? value.ToString()
                                                                    : "Not Set!";
                                        };

            
            RegionManager.SetRegionManager(DetailsRegionControl, innerRegion);
            RegionManager.UpdateRegions();
        }

        public string ViewName
        {
            get { return "AreaA"; }
        }
    }
}