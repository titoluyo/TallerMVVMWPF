using System.Windows;
using WpfEncuestas.Common;
using WpfEncuestas.Util;

namespace WpfEncuestas.ViewModels
{
    public class SeccionListViewModel : ListViewModelBase<SeccionViewModel, PlantillaViewModel> // ObservableObject
    {
        #region Commands

        protected override void Agregar()
        {
            var seccion = new SeccionViewModel();
            seccion.Mode = Mode.Add;
            seccion.Container = this;

            Editando = true; //Visibility.Visible;
            Selected = seccion;
        }

        #endregion

    }
   
}
