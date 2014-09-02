using System;
using System.Collections.Generic;
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
using SFChallenge.Model;

namespace SFChallenge.Controls
{  
    public class SuperPersonControl : Control
    {
        static SuperPersonControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SuperPersonControl), new FrameworkPropertyMetadata(typeof(SuperPersonControl)));
        }

        public SuperPerson SuperPerson
        {
            get { return (SuperPerson)GetValue(SuperPersonProperty); }
            set { SetValue(SuperPersonProperty, value); }
        }

        public static readonly DependencyProperty SuperPersonProperty = DependencyProperty.Register("SuperPerson", typeof(SuperPerson), typeof(SuperPersonControl), new FrameworkPropertyMetadata(null));
    }
}
