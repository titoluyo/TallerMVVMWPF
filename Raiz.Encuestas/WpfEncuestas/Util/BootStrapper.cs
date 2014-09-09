using WpfEncuestas.Interface;
using WpfEncuestas.Sevice;
using WpfEncuestas.Views;

namespace WpfEncuestas.Util
{
    class BootStrapper
    {
        public static void Initialize()
        {
            
            ServiceProvider.RegisterServiceLocator(new UnityServiceLocator());

            ServiceProvider.Instance.Register<IModalDialog, NotificacionViewDialog>();


        }


    }
}
