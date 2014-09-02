using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.DataBinding.Model;

namespace Wpf.DataBinding.Infrastructure {

    public class PersonIsActiveDataTemplateSelector : DataTemplateSelector {

        public DataTemplate ActiveDataTemplate { get; set; }
        public DataTemplate InactiveDataTemplate { get; set; }

        public override System.Windows.DataTemplate SelectTemplate(Object item, System.Windows.DependencyObject container) {
            if (item is Person) {
                var person = (Person)item;
                return person.IsActive ? this.ActiveDataTemplate : this.InactiveDataTemplate;
            }
            return null;
        }
    }
}
