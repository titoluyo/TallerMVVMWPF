using System;
using System.Windows;
using System.Windows.Controls;
using ThePhoneCompany.Inventory.ThePhoneCompanyDataService;

namespace ThePhoneCompany.Inventory.Infrastructure {
    public class SearchResultDataTemplateSelector : DataTemplateSelector {

        public DataTemplate ItemDataTemplate { get; set; }
        public DataTemplate CategoryDataTemplate { get; set; }

        public SearchResultDataTemplateSelector() {
        }

        public override DataTemplate SelectTemplate(Object item, DependencyObject container) {
            if(item is Item) {
                return this.ItemDataTemplate;
            }
            if(item is Category) {
                return this.CategoryDataTemplate;
            }
            return null;
        }
    }
}
