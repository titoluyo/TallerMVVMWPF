using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfEncuentas.Data
{
    interface IDataManager
    {

        INotificacionManager GetNotificacionManager();

    }
}
