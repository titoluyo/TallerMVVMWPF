using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevLake.MasterDetail.UI;
using DevLake.MasterDetail.UI.Common;
using System.ComponentModel;
using System.Windows.Input;

namespace DevLake.MasterDetail.UI.ViewModel
{
    public class OrderViewModel : ViewModelBase, IDataErrorInfo
    {
        private string description;
        private string quantity;

        private ICommand updateCommand;
        private ICommand deleteCommand;
        private ICommand cancelCommand;

        private OrderViewModel originalValue;

        public CustomerViewModel Customer
        {
            get;
            set;
        }

        public int OrderId
        {
            get;
            set;
        }

        public string Description
        {
            get { return description; }
            set 
            { 
                description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Quantity
        {
            get { return quantity; }
            set 
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        /// <summary>
        /// For determining if you are Adding or Editing an existing order
        /// </summary>
        public Mode Mode
        {
            get;
            set;
        }        

        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new CommandBase(i => this.Update(), null);
                }
                return updateCommand;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new CommandBase(i => this.Delete(), null);
                }
                return deleteCommand;
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new CommandBase(i => this.Undo(), null);
                }
                return cancelCommand;
            }
        }

        internal OrderViewModel(svcOrder.Order i)
        {
            OrderId = i.OrderId;
            description = i.Description;
            quantity = i.Quantity.ToString();
            //copy the current value so in case cancel you can undo
            this.originalValue = (OrderViewModel)this.MemberwiseClone();
        }

        internal OrderViewModel()
        {
        }

        private void Update()
        {
            svcOrder.OrderServiceClient c = new svcOrder.OrderServiceClient();
            if (this.Mode == ViewModel.Mode.Add)  //if adding an order
            {
                c.AddOrder(this.Customer.CustomerId,
                           new svcOrder.Order{
                                                Description = this.Description,
                                                Quantity = int.Parse(this.Quantity)
                                            });
                this.Customer.Orders = this.Customer.GetOrders();
            }
            else if (this.Mode == ViewModel.Mode.Edit)  //if editing an order
            {
                c.UpdateOrder(new svcOrder.Order
                                {
                                    OrderId = this.OrderId,
                                    Description = this.Description,
                                    Quantity = int.Parse(this.Quantity)
                                });
                //copy the current value so in case cancel you can undo
                this.originalValue = (OrderViewModel)this.MemberwiseClone();
            }
        }

        private void Delete()
        {
            svcOrder.OrderServiceClient c = new svcOrder.OrderServiceClient();
            c.DeleteOrder(this.OrderId);
            //refresh customer's list view
            this.Customer.Orders = this.Customer.GetOrders();
        }

        private void Undo()
        {
            //only if we are editing an order and decide to cancel 
            if (this.Mode == ViewModel.Mode.Edit)
            {
                this.Description = originalValue.Description;
                this.Quantity = originalValue.Quantity;
            }
        }


        #region IDataErrorInfo Members

        string IDataErrorInfo.this[string columnName]
        {
            get
            {   
                if (columnName == "Description")
                {
                    if (Description == null)  //must have an order description
                        return "Please enter a description";
                    else if (Description.Trim() == string.Empty)
                        return "Description is Required";
                }
                else if (columnName == "Quantity")
                {
                    int quantity;
                    if (!int.TryParse(this.Quantity, out quantity))  //if not integer
                        return "Quantity must be an integer";
                    else
                    {
                        if (quantity < 1)
                            return "Quantity must be at least 1";
                    }
                }
                return null;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return string.Empty; }
        }

        #endregion
    }
}
