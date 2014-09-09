using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MicroMvvm;
using WpfEncuestas.Util;


namespace WpfEncuestas.ViewModels
{
    public class PreguntaViewModel : ObservableObject
    {
        #region ctor

        public PreguntaViewModel()
        {
            OpcionList = new OpcionListViewModel();
        }

        #endregion

        #region Propiedades Modelo
        //public ObservableCollection<OpcionViewModel> Opciones { get; set; }
        private OpcionListViewModel _opcionList;

        private SeccionViewModel _seccion;
       
        private string _pregunta;
        private String _variable;
        //private VariableViewModel _variable;
       

        public ObservableCollection<VariableViewModel> Variables
        {
            get { return Container.Container.Container.Container.Variables; }
        }
        
        public SeccionViewModel Seccion
        {
            get { return _seccion; }
            set
            {
                if (_seccion==value) return;
                _seccion = value;
                RaisePropertyChanged(() => Seccion);
            }
        }

        public string Pregunta
        {
            get { return _pregunta; }
            set
            {
                if (_pregunta==value) return;
                _pregunta = value;
                RaisePropertyChanged(() => Pregunta);
            }
        }

        public String Variable
        {
            get { return _variable; }
            set
            {
                if(_variable==value) return;
                _variable = value;
                RaisePropertyChanged("Variable");
            }
        }

        public OpcionListViewModel OpcionList
        {
            get { return _opcionList; }
            set
            {
                if(_opcionList == value) return;
                _opcionList = value;
                RaisePropertyChanged("OpcionList");
            }
        }

        #endregion

        #region Propiedades y campos otros
        public PreguntaListViewModel Container { get; set; }
        public Mode Mode { get; set; }
        private PreguntaViewModel _originalValue;

        
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
            this._originalValue = (PreguntaViewModel)this.MemberwiseClone();
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
                _originalValue = (PreguntaViewModel)this.MemberwiseClone();
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
            Seccion = _originalValue.Seccion;
            Pregunta = _originalValue.Pregunta;
            Variable = _originalValue.Variable;
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
