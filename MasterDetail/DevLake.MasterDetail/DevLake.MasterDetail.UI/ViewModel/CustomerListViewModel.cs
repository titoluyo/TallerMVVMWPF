using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using DevLake.MasterDetail.UI;
using DevLake.MasterDetail.UI.Common;
using System.Windows.Input;
using DevLake.MasterDetail.UI.Interface;
using DevLake.MasterDetail.UI.Service;


namespace DevLake.MasterDetail.UI.ViewModel
{
    public class CustomerListViewModel : ViewModelBase
    {
        private static CustomerListViewModel instance = null;

        //the selected customer (for showing orders for that customer)
        private CustomerViewModel selectedCustomer = null;

        //the complete customer list
        private ObservableCollection<CustomerViewModel> customerList = null;

        //for opening up the Add Customer window
        private ICommand showAddCommand;

        public ObservableCollection<CustomerViewModel> CustomerList 
        {
            get
            {
                return GetCustomers();
            }
            set
            {
                customerList = value;
                OnPropertyChanged("CustomerList");
            }
        }

        //the currently selected customer
        public CustomerViewModel SelectedCustomer 
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                selectedCustomer = value;
                OnPropertyChanged("SelectedCustomer");
            }
        }

        public ICommand ShowAddCommand
        {
            get
            {
                if (showAddCommand == null)
                {
                    showAddCommand = new CommandBase(i => this.ShowAddDialog(), null);
                }
                return showAddCommand;
            }
        }

        private CustomerListViewModel()
        {
            this.CustomerList = GetCustomers();
        }

        public static CustomerListViewModel Instance()
        {
            if (instance == null)
                instance = new CustomerListViewModel();
            return instance;
        }

        internal ObservableCollection<CustomerViewModel> GetCustomers()
        {
            if (customerList == null)
                customerList = new ObservableCollection<CustomerViewModel>();
            customerList.Clear();
            foreach (UI.svcCustomer.Customer i in new UI.svcCustomer.CustomerServiceClient().GetCustomers())
            {
                CustomerViewModel c = new CustomerViewModel(i);
                customerList.Add(c);
            }
            return customerList;
        }

        private void ShowAddDialog()
        {
            CustomerViewModel customer = new CustomerViewModel();
            customer.Mode = Mode.Add;

            IModalDialog dialog = ServiceProvider.Instance.Get<IModalDialog>();
            dialog.BindViewModel(customer);
            dialog.ShowDialog();
        } 

    }
}
