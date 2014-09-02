using System;
using System.Windows.Controls;
using Common;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;

namespace ModuleA
{
    /// <summary>
    /// Interaction logic for AreaB.xaml
    /// </summary>
    public partial class AreaB : UserControl, IActiveAware
    {
        private readonly IRegionManager _regionManager;

        private bool _isActive;

        public AreaB(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeComponent();
        }

        #region IActiveAware Members

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;

                if (_isActive)
                {
                    var content = new AreaBContent();
                    _regionManager.Regions[RegionNames.ContentRegion].Add(content);
                    _regionManager.Regions[RegionNames.ContentRegion].Activate(content);
                }
            }
        }

        public event EventHandler IsActiveChanged;

        #endregion
    }
}