using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using MicroMvvm;

namespace WpfEncuestas.ViewModels
{
    public class SeccionBusquedaViewModel : ObservableObject
    {
        //private ObservableCollection<SeccionViewModel> _secciones;
        private SeccionListViewModel _seccionList;

        public SeccionBusquedaViewModel(SeccionListViewModel seccionList)
        {
            _seccionList = seccionList;
        }

        private string _textoBusqueda;

        public string TextoBusqueda
        {
            get { return _textoBusqueda; }
            set
            {
                if(_textoBusqueda== value) return;
                _textoBusqueda = value;
                RaisePropertyChanged(() => TextoBusqueda);
            }
        }

        private ICommand _buscarCommand;

        public ICommand BuscarCommand
        {
            get { return _buscarCommand ?? (_buscarCommand = new CommandBase(i => Buscar(), null)); }
        }

        private void Buscar()
        {
            _seccionList.Items =
                new ObservableCollection<SeccionViewModel>(
                    _seccionList.Items.Where(t => t.Nombre.Contains(_textoBusqueda)));

        }

    }
}
