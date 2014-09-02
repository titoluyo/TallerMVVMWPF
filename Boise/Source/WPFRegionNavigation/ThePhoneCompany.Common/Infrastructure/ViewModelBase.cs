using System.Windows;
using System.Windows.Threading;

namespace ThePhoneCompany.Common.Infrastructure {
    public abstract class ViewModelBase : ObservableObject {

        readonly Dispatcher _dispatcher;

       protected Dispatcher Dispatcher {
            get { return _dispatcher; }
        }

       public ViewModelBase() {
           _dispatcher = Application.Current != null ? Application.Current.Dispatcher : Dispatcher.CurrentDispatcher;
       }
    }
}
