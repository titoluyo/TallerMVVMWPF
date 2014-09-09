using System.Windows;
using System.Windows.Input;
using MicroMvvm;
using WpfEncuestas.Util;

namespace WpfEncuestas.ViewModels
{
    public class OpcionViewModel : ObservableObject
    {
        #region Propiedades Modelo
        private string _opcion;
        private int _ponderacion;

        public string Opcion
        {
            get { return _opcion; }
            set
            {
                if(_opcion==value)return;
                _opcion = value;
                RaisePropertyChanged("Opcion");
            }
        }

        public int Ponderacion
        {
            get { return _ponderacion; }
            set
            {
                if(_ponderacion==value)return;
                _ponderacion = value;
                RaisePropertyChanged("Ponderacion");
            }
        }
        #endregion

        #region Propiedades y campos otros
        public OpcionListViewModel Container { get; set; }
        public Mode Mode { get; set; }
        private OpcionViewModel _originalValue;


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
            this._originalValue = (OpcionViewModel)this.MemberwiseClone();
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
                _originalValue = (OpcionViewModel)this.MemberwiseClone();
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
            Container.Editando = false; //Visibility.Hidden;
        }

        protected void CancelarImplementacion()
        {
            Opcion = _originalValue.Opcion;
            Ponderacion = _originalValue.Ponderacion;
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
