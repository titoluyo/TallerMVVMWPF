using System.Windows;
using WpfEncuestas.Common;
using WpfEncuestas.Util;

namespace WpfEncuestas.ViewModels
{
    public class SeccionListViewModel : ListViewModelBase<SeccionViewModel, PlantillaViewModel> // ObservableObject
    {
        public SeccionListViewModel()
        {
            SeccionBusqueda = new SeccionBusquedaViewModel(this);
        }

        #region Commands

        protected override void Agregar()
        {
            var seccion = new SeccionViewModel();
            seccion.Mode = Mode.Add;
            seccion.Container = this;

            Editando = true; //Visibility.Visible;
            Selected = seccion;
        }

        #endregion

        #region Busqueda

        private SeccionViewModel _seccionEncontrada;

        public SeccionViewModel SeccionEncontrada
        {
            get { return _seccionEncontrada; }
            set
            {
                if (_seccionEncontrada == value) return;
                _seccionEncontrada = value;
                RaisePropertyChanged(() => SeccionEncontrada);
            }
        }

        private SeccionBusquedaViewModel _seccionBusqueda;
        public SeccionBusquedaViewModel SeccionBusqueda
        {
            get { return _seccionBusqueda; }
            set
            {
                if(_seccionBusqueda == value)return;
                _seccionBusqueda = value;
                RaisePropertyChanged(() => SeccionBusqueda);
            }
        }

        #endregion

    }
   
}
