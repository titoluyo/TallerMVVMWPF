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

namespace WpfModelValidation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Person person = new Person
                {
                    PersonName = "Tom",
                    Age = 31 //A model error should be occurred because Tom must be 30
                };

            DataContext = person;
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            Person person = DataContext as Person;
            if (person!=null)
            {
                person.Validate();
            }
        }
    }
}
