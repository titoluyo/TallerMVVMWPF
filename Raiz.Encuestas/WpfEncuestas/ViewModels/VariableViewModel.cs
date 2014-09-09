using MicroMvvm;

namespace WpfEncuestas.ViewModels
{
    public class VariableViewModel :ObservableObject
    {

        public ProcesoViewModel Proceso { get; set; }

        

        public string Codigo
        {
            get { return _codigo; }
            set
            {
                if(_codigo==value) return;
                _codigo = value;
                RaisePropertyChanged("Codigo");
            }
        }

        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if(_nombre==value)return;
                _nombre = value;
                RaisePropertyChanged("Nombre");
            }
        }

        private string _codigo;
        private string _nombre;
    }
}
