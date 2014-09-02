using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Wpf.Validation.Infrastructure {

    public class MaintenanceFormViewBase : UserControl {

        public MaintenanceFormViewBase() {
            // this adds a form level handler and will listen for each control that has the NotifyOnValidationError=True and a ValidationError occurs.
            this.Loaded += (s, e) => this.AddHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(ExceptionValidationErrorHandler), true);
            this.Unloaded += (s, e) => this.RemoveHandler(System.Windows.Controls.Validation.ErrorEvent, new RoutedEventHandler(ExceptionValidationErrorHandler));
        }

        void ExceptionValidationErrorHandler(Object sender, RoutedEventArgs e) {
            var args = e as ValidationErrorEventArgs;
            if (args == null || args.Error == null || !(args.Error.RuleInError is ExceptionValidationRule)) {
                return;
            }

            var maintenanceFormViewModelBase = this.DataContext as MaintenanceFormViewModelBase;
            if (maintenanceFormViewModelBase == null) {
                return;
            }

            //we only want to work with validation errors that are Exceptions because the business object has already recorded the business rule violations using IDataErrorInfo.
            var bindingExpression = (BindingExpression)args.Error.BindingInError;
            var dataItemName = bindingExpression.DataItem.ToString();
            var propertyName = bindingExpression.ParentBinding.Path.Path;
            var sb = new System.Text.StringBuilder(1024);

            foreach (var ve in System.Windows.Controls.Validation.GetErrors((DependencyObject)args.OriginalSource).Where(ve => (ve.RuleInError is ExceptionValidationRule))) {
                sb.AppendFormat("{0} has error ", propertyName);

                if (ve.Exception == null || ve.Exception.InnerException == null) {
                    sb.AppendLine(ve.ErrorContent.ToString());
                } else {
                    sb.AppendLine(ve.Exception.InnerException.Message);
                }
            }

            switch (args.Action) {
                case ValidationErrorEventAction.Added:
                    maintenanceFormViewModelBase.AddViewValidationError(new ViewValidationError(dataItemName, propertyName, sb.ToString()));
                    break;
                case ValidationErrorEventAction.Removed:
                    maintenanceFormViewModelBase.RemoveViewValidationError(new ViewValidationError(dataItemName, propertyName, sb.ToString()));
                    break;
                default:
                    throw new Exception("Action value was not programmed: " + args.Action);
            }
        }
    }
}
