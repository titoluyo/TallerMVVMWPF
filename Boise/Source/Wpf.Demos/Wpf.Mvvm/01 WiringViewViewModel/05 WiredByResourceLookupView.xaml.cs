﻿using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Wpf.Common.Unity;

namespace Wpf.Mvvm.WiringViewViewModel {

    [RegionMemberLifetime(KeepAlive = false)]
    [ViewContainerInitializer(typeof(WiredByResourceLookupViewModel))]
    public partial class WiredByResourceLookupView : UserControl {

        public WiredByResourceLookupView() {
            InitializeComponent();
        }
    }
}
