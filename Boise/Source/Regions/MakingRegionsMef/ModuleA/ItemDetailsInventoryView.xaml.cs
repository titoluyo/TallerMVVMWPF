﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common;

namespace ModuleA
{
    /// <summary>
    /// Interaction logic for ItemDetailsInventoryView.xaml
    /// </summary>
    [ExportView(RegionName = "ItemDetailsRegion")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ItemDetailsInventoryView : UserControl
    {
        public ItemDetailsInventoryView()
        {
            InitializeComponent();
        }
    }
}