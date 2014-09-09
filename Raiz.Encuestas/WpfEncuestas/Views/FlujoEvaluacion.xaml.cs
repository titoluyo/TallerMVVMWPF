using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfEncuestas.ViewModels;

namespace WpfEncuestas.Views
{
    /// <summary>
    /// Interaction logic for FlujoEvaluacion.xaml
    /// </summary>
    public partial class FlujoEvaluacion : Window
    {
        public FlujoEvaluacion()
        {
            InitializeComponent();
            this.DataContext = ProcesoViewModel.Instance();
        }
    }
}
