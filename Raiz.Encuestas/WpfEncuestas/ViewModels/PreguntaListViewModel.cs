using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MicroMvvm;
using WpfEncuestas.Common;
using WpfEncuestas.Util;


namespace WpfEncuestas.ViewModels
{
    public class PreguntaListViewModel : ListViewModelBase<PreguntaViewModel,SeccionViewModel>
    {
        #region Commands

        protected override void Agregar()
        {
            var item = new PreguntaViewModel();
            item.Mode = Mode.Add;
            item.Container = this;

            Editando = true; //Visibility.Visible;
            Selected = item;
        }

        #endregion
    }
}
