using System;
using WpfEncuestas.Interface;

namespace WpfEncuestas.Views
{
    public class NotificacionViewDialog : IModalDialog
    {

        private Views.Window1 view;

        void IModalDialog.BindViewModel<TViewModel>(TViewModel viewModel)
        {
            GetDialog().DataContext = viewModel;
        }

        void IModalDialog.ShowDialog()
        {
            GetDialog().ShowDialog();
        }

        void IModalDialog.Close()
        {
            GetDialog().Close();
        }


        private Views.Window1 GetDialog()
        {
            if (view == null)
            {
                //create the view if the view does not exist
                view = new Views.Window1();
                view.Closed += new EventHandler(view_Closed);
            }
            return view;
        }

        void view_Closed(object sender, EventArgs e)
        {
            view = null;
        }


    }
}
