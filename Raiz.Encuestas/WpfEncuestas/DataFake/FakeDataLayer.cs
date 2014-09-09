using System.Collections.ObjectModel;
using WpfEncuestas.ViewModels;

namespace WpfEncuestas.DataFake
{
    class FakeDataLayer
    {


        public static ObservableCollection<NotificacionViewModel> GetPeopleFromDatabase()
        {
            //Simulate database extaction
            //For example from ADO DataSets or EF
            return new ObservableCollection<NotificacionViewModel>
            {
                new NotificacionViewModel { CodigoNotificacion = "Notificacion01",
                    DescripcionNotificacion = "Mensual",
                    NumerodiaNotificacion = "4",
                    FechaNotificacion = true,
                    PrioridadNotificacion = "Urgente" },
                new NotificacionViewModel { CodigoNotificacion = "Notificacion02",
                    DescripcionNotificacion = "Parcial",
                    NumerodiaNotificacion = "4",
                    FechaNotificacion = false,
                    PrioridadNotificacion = "Urgente" },
                
            };
        }







    }
}
