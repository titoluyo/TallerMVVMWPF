using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroMvvm;

namespace WpfEncuestas.ViewModels
{
    public class ZonaViewModel : ObservableObject
    {

        private int _codigozona;
        private string _zona;

        public int Codigozona
        {
            get { return _codigozona; }
            set
            {
                if (_codigozona == value) return;
                _codigozona = value;
                RaisePropertyChanged("Codigozona");
            }
        }

        public string Zona
        {
            get { return _zona; }
            set
            {
                if (_zona == value) return;
                _zona = value;
                RaisePropertyChanged("Zona");
            }
        }



    }
}
