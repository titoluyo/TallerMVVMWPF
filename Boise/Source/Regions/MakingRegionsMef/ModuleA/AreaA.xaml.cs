using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Common;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;

namespace ModuleA
{
    /// <summary>
    /// Interaction logic for AreaA.xaml
    /// </summary>
    [ExportView(RegionName = RegionNames.NavigationRegion)]
    [ViewSortHint("01")]
    public partial class AreaA : UserControl, IActiveAware
    {
        private readonly IRegionManager _regionManager;

        private bool _isActive;

        [ImportingConstructor]
        public AreaA(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeComponent();
        }

        #region IActiveAware Members

        public bool IsActive
        {
            get { return _isActive; }
            set { 
                _isActive = value;

                IRegion contentRegion = _regionManager.Regions[RegionNames.ContentRegion];
                if (_isActive)
                {
                    var content = new AreaAContent(_regionManager);
                    contentRegion.Add(content);
                    contentRegion.Activate(content);
                }
            }
        }

        public event EventHandler IsActiveChanged;

        #endregion

        
    }
}