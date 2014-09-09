using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WpfEncuestas.Util;
using WpfEncuestas.Views;

namespace WpfEncuestas
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Views.ProcesoView window = new Views.ProcesoView();
            Views.PlantillaEvaluacionView window = new PlantillaEvaluacionView();
            //Views.FlujoEvaluacion window = new Views.FlujoEvaluacion();


            window.Show();

            

            BootStrapper.Initialize();



        }

    }
}
