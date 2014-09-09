using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WpfEncuestas.ViewModels;
using System.Collections.ObjectModel;


namespace WpfEncuestas.DataFake
{
    public static class PeriodosFake
    {


        public static ProcesoViewModel CreateInstance()
        {

            var proceso = new ProcesoViewModel();



            var periodo1 = new PeriodosViewModel()
            {
                CodigoPeriodo = 1,
                DescripcionPeriodo = "2014-02",

            };
            var periodo2 = new PeriodosViewModel()
            {
                CodigoPeriodo = 2,
                DescripcionPeriodo = "2014-03",

            };
            var periodo3 = new PeriodosViewModel()
            {
                CodigoPeriodo = 3,
                DescripcionPeriodo = "2014-04",

            };


            //Add to Collection
            proceso.PeriodosList.Items = new ObservableCollection<PeriodosViewModel>()
            {
                periodo1,
                periodo2,
                periodo3,
                
            };

            return proceso;
        }






    }
}



//var periodos = new ProcesoViewModel();

        //periodos.Periodos = new ObservableCollection<PeriodosViewModel>
        //{

        //    new PeriodosViewModel
        //    {
        //        CodigoPeriodo = 1,
        //        DescripcionPeriodo = "2014-02"
        //    },
        //    new PeriodosViewModel
        //    {
        //        CodigoPeriodo = 2,
        //        DescripcionPeriodo = "2014-03"

        //    },
        //    new PeriodosViewModel
        //    {
        //        CodigoPeriodo = 3,
        //        DescripcionPeriodo = "2014-04"
        //    }
        //};


        //return null;

    