using System.Windows;
using WpfEncuestas.Common;
using WpfEncuestas.Util;

namespace WpfEncuestas.ViewModels
{
    public class OpcionListViewModel : ListViewModelBase<OpcionViewModel,PreguntaViewModel>
    {
        #region Commands

        protected override void Agregar()
        {
            var opcion = new OpcionViewModel();
            opcion.Mode = Mode.Add;
            opcion.Container = this;

            Editando = true; //Visibility.Visible;
            Selected = opcion;
        }

        #endregion
    }
}
