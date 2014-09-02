using System;
using System.Collections.Generic;
using System.Windows.Data;
using ThePhoneCompany.Common.DataGeneration;
using ThePhoneCompany.Common.Infrastructure;
using ThePhoneCompany.Inventory.ThePhoneCompanyDataService;

namespace ThePhoneCompany.Inventory.SampleData {

    public class InventoryViewModelSampleData {

#if DEBUG
        readonly CollectionViewSource _dataItems = new CollectionViewSource();
        readonly CollectionViewSource _activeDataItems = new CollectionViewSource();

        public CollectionViewSource DataItems {
            get { return _dataItems; }
        }

        public CollectionViewSource ActiveDataItems {
            get { return _activeDataItems; }
        }

        public Boolean ItemIsChecked {
            get { return true; }
            set { }
        }

        public Boolean CategoryIsChecked {
            get { return false; }
            set { }
        }

        public String ItemText {
            get {
                return "Item (3)";
            }
        }

        public String CategoryText {
            get {
                return "Category";
            }
        }

        public InventoryViewModelSampleData() {
            var data = new DataGenerator();
            data.SeedSequentialInteger(100,1);
            var dataItems = new List<Item>();

            for(int i = 1; i < 100; i++) {

                dataItems.Add(
                    new Item { Description = data.GetString(50), 
                                 ItemID=data.GetSequentialInteger(), 
                                 Price=data.GetDecimal(5,300) });
                
            }

            _dataItems.Source = dataItems;

            var activeItems = new List<NonLinearNavigationMetadata>();

            var mock = new Mock(dataItems[5].Description, "Editing", dataItems[5].ItemID.ToString());
            activeItems.Add(new NonLinearNavigationMetadata(new NonLinearNavigationMetadata(mock)));

            mock = new Mock(dataItems[7].Description, "Editing", dataItems[7].ItemID.ToString());
            activeItems.Add(new NonLinearNavigationMetadata(new NonLinearNavigationMetadata(mock)));

            mock = new Mock("Windows Phone 7", "Adding", "0");
            activeItems.Add(new NonLinearNavigationMetadata(new NonLinearNavigationMetadata(mock)));

            _activeDataItems.Source = activeItems;
        }

        class Mock : INonLinearNavigationObject {
            readonly String _title;
            readonly String _state;
            readonly String _key;
            readonly String _uriString;
            readonly String _application;

            public string Title {
                get { return _title; }
            }

            public string State {
                get { return _state; }
            }

            public string Key {
                get { return _key; }
            }

            public string UriString {
                get { return _uriString; }
            }

            public string Application {
                get { return _application; }
            }

            public Mock(String title, String state, String key) {
                _title = title;
                _state = state;
                _key = key;
                _uriString = String.Empty;
                _application = String.Empty;
            }
        }
#endif
    }
}
