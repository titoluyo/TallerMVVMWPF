using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using MicroMvvm;
using WpfEncuestas.ViewModels;

namespace WpfEncuestas.Common
{
    public abstract class ListViewModelBase<TItem,TContainer> : ObservableObject
    {
        #region ctor

        public ListViewModelBase()
        {
            Editando = false;
            Items = new ObservableCollection<TItem>();
        }

        #endregion

        private ObservableCollection<TItem> _items;

        public ObservableCollection<TItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                if (_items == value) return;
                _items = value;
                RaisePropertyChanged(() => this.Items);
            }
        }

        public TContainer Container { get; set; }

        #region Propiedades del view-model

        private TItem _selected;
        private bool _editando;

        public bool Editando
        {
            get
            {
                return _editando;
            }
            set
            {
                _editando = value;
                RaisePropertyChanged(() => Editando);
            }
        }

        public TItem Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                RaisePropertyChanged(() => Selected);
            }
        }

        #endregion

        #region Commands
        private ICommand _agregarCommand;

        public ICommand AgregarCommand
        {
            get
            {
                return _agregarCommand ?? (_agregarCommand = new CommandBase(i => Agregar(), null));
            }
        }

        protected virtual void Agregar() { }

        #endregion
    }
}
