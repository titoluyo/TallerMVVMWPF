using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEncuestas.ViewModels;

namespace WpfEncuestas.DataFake
{
    public static class ProcesoFake
    {

        public static ProcesoViewModel CreateInstance()
        {
            var proceso = new ProcesoViewModel();

            proceso.NombreProceso = "Proceso 1";
            proceso.DescripcionProceso = "Proceso de evaluación de personal";
            proceso.FechaIniProceso = "10/02/2014";
            proceso.FechaFinProceso = "10/04/2014";
            proceso.PeriodicidadProceso = 4;

            proceso.FechaIniPeriodo = "7"; //Dia de inicio del periodo ---> Todos los periodos comoenzaran cada 7 de cada mes
            proceso.HoraIniPeriodo = "15:00";
            proceso.DuraciondiasPeriodo = 2;
            proceso.DuracionhorasPeriodo = 1;
            proceso.FechaFinPeriodo = "17/02/2014";
            proceso.PeriodoActual = "Febereo 2014";



            proceso.ListaNotificaciones = new ObservableCollection<NotificacionViewModel>
            {
                new NotificacionViewModel { CodigoNotificacion = "Notificacion01",
                    DescripcionNotificacion = "Mensual",
                    NumerodiaNotificacion = "4",
                    FechaNotificacion = true,
                    PrioridadNotificacion = "Urgente" },
                new NotificacionViewModel { CodigoNotificacion = "Notificacion02",
                    DescripcionNotificacion = "Parcial",
                    NumerodiaNotificacion = "4",
                    FechaNotificacion = false,
                    PrioridadNotificacion = "Urgente" },
                
            };


            //Calling by PeriodosViewModel
            var periodo1 = new PeriodosViewModel
            {
                CodigoPeriodo = 1,
                DescripcionPeriodo = "2014-02",
                Container = proceso.PeriodosList,
            };


            var periodo2 = new PeriodosViewModel
            {
                CodigoPeriodo = 2,
                DescripcionPeriodo = "2014-03",
                Container = proceso.PeriodosList,
            };

            var periodo3 = new PeriodosViewModel
            {
                CodigoPeriodo = 3,
                DescripcionPeriodo = "2014-04",
                Container = proceso.PeriodosList,
            };

            //Add to Collection- Items

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
