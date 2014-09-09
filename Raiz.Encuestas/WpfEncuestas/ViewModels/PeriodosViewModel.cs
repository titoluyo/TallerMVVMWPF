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

        private int _codigoPeriodo;
        private string _descripcionPeriodo;

        //private ProcesoViewModel viewmodelProceso;


        // Lista de Periodos

        public int CodigoPeriodo
        {
            get { return _codigoPeriodo; }
            set
            {
                if(_codigoPeriodo == value) return;;
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

        
        public PeriodoListaViewModel Container { get; set; }


        #endregion


    }
}
