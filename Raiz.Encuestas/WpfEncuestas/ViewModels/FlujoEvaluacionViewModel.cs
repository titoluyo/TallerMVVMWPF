using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroMvvm;
using WpfEncuestas.Views;

namespace WpfEncuestas.ViewModels
{
    public class FlujoEvaluacionViewModel : ObservableObject
    {



        public FlujoEvaluacionViewModel()
        {
            
        }


        private int _codigoRol;
        private string _nombreRol;



        public int CodigoRol
        {
            get { return _codigoRol; }
            set
            {
                if(_codigoRol == value)return;
                _codigoRol = value;
                RaisePropertyChanged("CodigoRol");
            }
        
        }



        public string NombreRol
        {
            get { return _nombreRol; }
            set
            {
               if(_nombreRol == value)return;
                _nombreRol = value;
                RaisePropertyChanged("NombreRol");
            }
        }


        //=================================================
       


        public FlujoEvaluacionListViewModel Container { get; set; }

        
    }
}
