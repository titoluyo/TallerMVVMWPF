using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfEncuestas.ViewModels;

namespace WpfEncuestas.DataFake
{
    
    
    public static class PlantillaFake
    {

        public static PlantillaViewModel CreateInstance()
        {

            var plantilla = new PlantillaViewModel();

            plantilla.Titulo = "Evaluacíón de Desempeño";
            plantilla.Mensaje = "Complete el siguiente formulario";
            plantilla.IdEvaluacion = "P001";
            plantilla.Periodo = " ";
            plantilla.TodosPeriodos = true;

            //plantilla.SeccionList = new SeccionListViewModel();
            plantilla.SeccionList.Container = plantilla;

            plantilla.Variables = new ObservableCollection<VariableViewModel>
            {
                new VariableViewModel {Codigo = "1", Nombre = "Desempeño"},
                new VariableViewModel {Codigo = "2", Nombre = "Efectividad"},
                new VariableViewModel {Codigo = "3", Nombre = "Cumplimiento"}
            };


            //var algo = new PlantillaViewModel();
            
            
            
            var seccion1 = new SeccionViewModel
            {
                Orden = 1,
                Nombre = "Sección 9",
                Container = plantilla.SeccionList,
                //PreguntaList = new PreguntaListViewModel
                //{
                //    Items = new ObservableCollection<PreguntaViewModel>(),
                    
                //}
            };
            
            //seccion1.PreguntaList.Container = seccion1;
            //seccion1.PreguntaList.Items.Add(new PreguntaViewModel { Container = seccion1.PreguntaList, Pregunta = "Como te llamas?", Seccion = seccion1, Variable = "Desempeño", OpcionList = new OpcionListViewModel{Items = new ObservableCollection<OpcionViewModel>()}});
            //seccion1.PreguntaList.Items.Add(new PreguntaViewModel { Container = seccion1.PreguntaList, Pregunta = "Cual es tu puesto?", Seccion = seccion1, Variable = "Desempeño", OpcionList = new OpcionListViewModel { Items = new ObservableCollection<OpcionViewModel>() } });
            //seccion1.PreguntaList.Items.Add(new PreguntaViewModel { Container = seccion1.PreguntaList, Pregunta = "Pregunta 3?", Seccion = seccion1, Variable = "Desempeño", OpcionList = new OpcionListViewModel { Items = new ObservableCollection<OpcionViewModel>() } });

            plantilla.SelectedSeccionPregunta = seccion1;

            

            //Agragando a la Collecion
            plantilla.SeccionList.Items = new ObservableCollection<SeccionViewModel>()
            {
                seccion1,
                new SeccionViewModel 
                {Orden = 2, 
                    Nombre = "Sección 3", 
                    Container = plantilla.SeccionList, 
                    PreguntaList = new PreguntaListViewModel
                    {
                        Items = new ObservableCollection<PreguntaViewModel>()
                    }}
            };
            

            return plantilla;
        }


    }
}
