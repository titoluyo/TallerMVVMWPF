using System.Windows;
using System.Windows.Input;
using MicroMvvm;
using WpfEncuestas.Common;
using WpfEncuestas.Util;
using WpfEncuestas.Views;

namespace WpfEncuestas.ViewModels
{
    public class SeccionViewModel : Item1ViewModelBase<SeccionViewModel> //ObservableObject
    {
        #region ctor

        public SeccionViewModel()
        {
            PreguntaList = new PreguntaListViewModel();
            Buscando = Visibility.Hidden;
        }

        #endregion

        #region Propiedades Modelo

        private string _nombre;
        private int _orden;
        private PreguntaListViewModel _preguntaList;
        
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                if (_nombre == value) return;
                _nombre = value;
                RaisePropertyChanged("Nombre");
            }
        }

        public int Orden
        {
            get { return _orden; }
            set
            {
                if (_orden == value) return;
                _orden = value;
                RaisePropertyChanged("Orden");
            }
        }

        public PreguntaListViewModel PreguntaList
        {
            get { return _preguntaList; }
            set
            {
                if(_preguntaList == value) return;
                _preguntaList = value;
                RaisePropertyChanged("PreguntaList");
            }
        }

        #endregion

        #region Propiedades y campos otros
        public SeccionListViewModel Container { get; set; }
        /*
        public Mode Mode { get; set; }
        protected SeccionViewModel _originalValue;
        */
        #endregion

        #region Commands
        private ICommand _editarCommand;
        public ICommand EditarCommand
        {
            get { return _editarCommand ?? (_editarCommand = new CommandBase(i => Editar(), null)); }
        }

        private void Editar()
        {
            this.Mode = Mode.Edit;
            this._originalValue = (SeccionViewModel)this.MemberwiseClone();
            this.Container.Selected = this;
            this.Container.Editando = true; //Visibility.Visible;
        }
        /*
        private ICommand _grabarCommand;

        public ICommand GrabarCommand
        {
            get { return _grabarCommand ?? (_grabarCommand = new CommandBase(i => Grabar(), null)); }
        }

        private void Grabar()
        {
            if (Mode == Mode.Add)
            {
                Container.Items.Add(this);
            }
            else
            {
                _originalValue = (SeccionViewModel)this.MemberwiseClone();
            }

            Container.Editando = false; //Visibility.Hidden;
        }

        private ICommand _cancelarCommand;

        public ICommand CancelarCommand
        {
            get { return _cancelarCommand ?? (_cancelarCommand = new CommandBase(i => Cancelar(), null)); }
        }

        private void Cancelar()
        {
            if (Mode == Mode.Edit)
            {

                CancelarImplementacion();
            }
            Container.Editando = false; // Visibility.Hidden;
        }

        protected void CancelarImplementacion()
        {
            Nombre = _originalValue.Nombre;
        }
        */
        private ICommand _eliminarCommand;

        public ICommand EliminarCommand
        {
            get { return _eliminarCommand ?? (_eliminarCommand = new CommandBase(i => Eliminar(), null)); }
        }

        private void Eliminar()
        {
            Container.Items.Remove(this);
        }

        #endregion

        private ICommand _buscarCommand;
        public ICommand BuscarCommand
        {
            get { return _buscarCommand ?? (_buscarCommand = new CommandBase(i => Buscar(), null)); }
        }

        //private SeccionListViewModel _busqueda;
        private Visibility _buscando;

        public Visibility Buscando
        {
            get { return _buscando; }
            set
            {
                if (_buscando == value)return;
                _buscando = value;
                RaisePropertyChanged(() => Buscando);
            }
        }
        private void Buscar()
        {
            //_busqueda = new SeccionListViewModel(this);
            Container.SeccionEncontrada = this;
            Buscando = Visibility.Visible;
        }

        private ICommand _seleccionarCommand;

        public ICommand SeleccionarCommand
        {
            get { return _seleccionarCommand ?? (_seleccionarCommand = new CommandBase(i => Seleccionar(), null)); }
        }

        private void Seleccionar()
        {
            Container.SeccionEncontrada.Nombre = this.Nombre;
            Container.SeccionEncontrada.Orden = this.Orden;
            //Container.SeccionEncontrada = (SeccionViewModel) this.MemberwiseClone(); // DON'T WORK!
            Buscando = Visibility.Hidden;
        }


    }
}
