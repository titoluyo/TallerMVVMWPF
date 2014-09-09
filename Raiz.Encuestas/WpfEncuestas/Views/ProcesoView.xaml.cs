using System.Windows;
using WpfEncuestas.ViewModels;

namespace WpfEncuestas.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ProcesoView : Window
    {
        public ProcesoView()
        {
            InitializeComponent();
            this.DataContext = ProcesoViewModel.Instance();
        }

        





        private void BtnAgregarfila_Click(object sender, RoutedEventArgs e)
        {

            // Agrega una fila a la grilla pero tienes que estar desactivado el Itemsource

            //var data = new ListPeriodos { Test1 = "Test1", Test2 = "Test2" };

            //GridLista.Items.Add(data);

            //Window1 win1= new Window1();
            //win1.Show();
            

        }



        private void BtnAgrega_Click(object sender, RoutedEventArgs e)
        {
           
        }
        

    }
}
