using System.Windows;
using System.Windows.Input;
using MicroMvvm;
using WpfEncuestas.Common;
using WpfEncuestas.Util;

namespace WpfEncuestas.ViewModels
{
    public class SeccionViewModel : ObservableObject
    {
        #region ctor

        public SeccionViewModel()
        {
            PreguntaList = new PreguntaListViewModel();
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
        public Mode Mode { get; set; }
        protected SeccionViewModel _originalValue;

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



    }
}
