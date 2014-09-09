using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfEncuestas.Util;

namespace WpfEncuestas.Common
{
    public interface IItemViewModel
    {
        Mode Mode { get; set; }

        ICommand GrabarCommand { get; }
        ICommand CancelarCommand { get; }

    }
}
