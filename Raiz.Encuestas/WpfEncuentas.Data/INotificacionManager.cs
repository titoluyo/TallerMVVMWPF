using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEncuestas;
using WpfEncuestas.ViewModels;
using VISTA=WpfEncuentas; 

namespace WpfEncuentas.Data
{
    public interface INotificacionManager
    {


        int Add(string codigoNotificacion, string descripNotificacion, string numerodiaNotificacion, Boolean fechaNotificacion, string prioridadNotificacion);
        void Delete(int codigoNotificacion);
        void Update(string codigoNotificacion, string descripNotificacion, string numerodiaNotificacion, Boolean fechaNotificacion, string prioridadNotificacion);
        List<NotificacionViewModel> FindAll();
        //Order Find(int orderId);    


    }
}
