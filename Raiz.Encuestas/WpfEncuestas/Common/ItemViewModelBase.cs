using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MicroMvvm;
using WpfEncuestas.Util;
using WpfEncuestas.ViewModels;

namespace WpfEncuestas.Common
{
    public class ItemViewModelBase<TItem,TList> : ObservableObject
        where TList : ListViewModelBase<TItem,TList>
    {
        #region Propiedades y campos otros
        public TList Container { get; set; }
        public Mode Mode { get; set; }
        protected TItem _originalValue;

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
            this._originalValue = (TItem)this.MemberwiseClone();
            //this.Container.Selected = this;
            this.Container.Select(this);
            this.Container.Editando = Visibility.Visible;
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
                _originalValue = (TItem)this.MemberwiseClone();
            }

            Container.Editando = Visibility.Hidden;
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
            Container.Editando = Visibility.Hidden;
        }

        protected virtual void CancelarImplementacion(){}

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
