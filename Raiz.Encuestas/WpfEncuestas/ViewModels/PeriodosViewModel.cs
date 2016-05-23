using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroMvvm;

namespace WpfEncuestas.ViewModels
{
    public class PeriodosViewModel : ObservableObject
    {


        #region Propiedades del Modelo

        private int _codigoProceso;
        private string _descripcionProceso;

        //private ProcesoViewModel viewmodelProceso;

        public PeriodosViewModel()
        {
         PeriodosxProcesoList = new PeriodoListaViewModel();
            
        }



        #region Calling Process
       
        private ProcesoViewModel _procesoLists;

        #endregion



        private PeriodoListaViewModel _periodoxprocesoList;

        public PeriodoListaViewModel PeriodosxProcesoList
        {

            get { return _periodoxprocesoList; }
            set
            {
                if (_periodoxprocesoList == value) return;
                _periodoxprocesoList = value;
                RaisePropertyChanged("PeriodosxProcesoList");
            }
        }


        // Lista de Numero de Procesos Guardados

        public int CodigoProceso
        {
            get { return _codigoProceso; }
            set
            {
                if(_codigoProceso == value) return;;
                _codigoProceso = value;
                RaisePropertyChanged("CodigoProceso");
            }
        }

        
        public string DescripcionProceso
        {
            get { return _descripcionProceso; }
            set
            {
                if(_descripcionProceso == value)return;
                _descripcionProceso = value;
                RaisePropertyChanged("DescripcionProceso");
            }
        }


        // Lista de Periodos Guardados por Proceso


        //==============================================

        private int _codigoPeriodo;
        private string _descripcionPeriodo;


        public int CodigoPeriodo
        {
            get { return _codigoPeriodo; }
            set
            {
                if(_codigoPeriodo == value) return;
                _codigoPeriodo = value;
                RaisePropertyChanged("CodigoPeriodo");
            
            }
        }


        public string DescripcionPeriodo
        {
            get { return _descripcionPeriodo; }
            set
            {
                if(_descripcionPeriodo == value)return;
                _descripcionPeriodo = value;
                RaisePropertyChanged("DescripcionPeriodo");
            }
        }


        //================================================


        
        public PeriodoListaViewModel Container { get; set; }

       

        #endregion


    }
}
