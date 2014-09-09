using MicroMvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfEncuestas.Util;

namespace WpfEncuestas.Common
{
    public class Item1ViewModelBase<TItem> : ObservableObject //, IItemViewModel
        //where T : IItemViewModel, new()
    {
        #region Propiedades y campos otros
        private Mode _mode;
        public Mode Mode
        {
            get { return _mode; }
            set
            {
                if (_mode == value) return;
                _mode = value;

            }
        }
        protected TItem _originalValue;
        #endregion

        #region Commands

        private ICommand _grabarCommand;

        public ICommand GrabarCommand
        {
            get { return _grabarCommand ?? (_grabarCommand = new CommandBase(i => Grabar(), null)); }
        }

        protected virtual void Grabar()
        {
            if (Mode == Mode.Edit)
            {
                _originalValue = (TItem)this.MemberwiseClone();
            }
            //Container.Editando = Visibility.Hidden;
            Mode = Mode.None;
        }

        private ICommand _cancelarCommand;

        public ICommand CancelarCommand
        {
            get { return _cancelarCommand ?? (_cancelarCommand = new CommandBase(i => Cancelar(), null)); }
        }

        protected virtual void Cancelar()
        {
            if (Mode == Mode.Edit)
            {
                CancelarImplementacion();
            }
            //Container.Editando = Visibility.Hidden;
            Mode = Mode.None;
        }

        protected virtual void CancelarImplementacion()
        {
            //Nombre = _originalValue.Nombre;
        }

        #endregion
    }
}
