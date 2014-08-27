using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevLake.MasterDetail.UI.Interface;

namespace DevLake.MasterDetail.UI.View
{
    public class CustomerViewDialog : IModalDialog
    {
        private CustomerView view;

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

        private CustomerView GetDialog()
        {
            if (view == null)
            {
                //create the view if the view does not exist
                view = new CustomerView();
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

