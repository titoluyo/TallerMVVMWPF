using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using ThePhoneCompany.Common.Commands;
using ThePhoneCompany.Common.Constants;
using ThePhoneCompany.Common.Infrastructure;
using ThePhoneCompany.Inventory.Business;
using ThePhoneCompany.Inventory.Events;

namespace ThePhoneCompany.Inventory.Views {

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class InventoryViewModel : NavigationAwareViewModelBase, IPartImportsSatisfiedNotification {

        #region Declarations

        Boolean _itemIsChecked;
        Boolean _categoryIsChecked;
        readonly CollectionViewSource _dataItems = new CollectionViewSource();
        readonly CollectionViewSource _activeDataItems = new CollectionViewSource();
        readonly IItemRepository _itemRepository;
        readonly ICategoryRepository _categoryRepository;
        readonly InventoryNavigatedEvent _inventoryNavigatedEvent;
        ICommand _editItemRecordCommand;
        ICommand _editCategoryRecordCommand;
        ICommand _navigateCommand;

        #endregion //Declarations

        #region Properties

        public CollectionViewSource DataItems {
            get { return _dataItems; }
        }

        public CollectionViewSource ActiveDataItems {
            get { return _activeDataItems; }
        }

        public Boolean ItemIsChecked {
            get { return _itemIsChecked; }
            set {
                _itemIsChecked = value;
                this.RaisePropertyChanged("ItemIsChecked");
                if(_itemIsChecked) {
                    LoadItems();
                }
            }
        }

        public Boolean CategoryIsChecked {
            get { return _categoryIsChecked; }
            set {
                _categoryIsChecked = value;
                this.RaisePropertyChanged("CategoryIsChecked");
                if(_categoryIsChecked) {
                    LoadCategories();
                }
            }
        }

        public String ItemText {
            get {
                return MakeLabelWithCountForApplicationView(typeof(ItemView), Constants.Item);
            }
        }

        public String CategoryText {
            get {
                return MakeLabelWithCountForApplicationView(typeof(CategoryView), Constants.Category);
            }
        }

        public ICommand EditItemRecordCommand {
            get { return _editItemRecordCommand ?? (_editItemRecordCommand = new RelayCommand<Int32>(itemId => this.NavigateRequest(Constants.MainContentRegion, typeof (ItemView).FullName, Constants.Key, itemId.ToString()))); }
        }

        public ICommand EditCategoryRecordCommand {
            get { return _editCategoryRecordCommand ?? (_editCategoryRecordCommand = new RelayCommand<Int32>(categoryId => this.NavigateRequest(Constants.MainContentRegion, typeof (CategoryView).FullName, Constants.Key, categoryId.ToString()))); }
        }

        public ICommand NavigateCommand {
            get { return _navigateCommand ?? (_navigateCommand = new RelayCommand<String>(uriString => this.NavigateRequest(Constants.MainContentRegion, uriString))); }
        }

        public ICommand AddCommand {
            get { return new RelayCommand(this.AddNavigate); }
        }

        public ICommand RefreshDataCommand {
            get { return new RelayCommand(this.RefreshData); }
        }
        #endregion //Properties

        #region Constructor

        [ImportingConstructor]
        public InventoryViewModel(
            IRegionManager regionManager, 
            IEventAggregator eventAggregator,
            IItemRepository itemRepository,
            ICategoryRepository categoryRepository): base(regionManager) {

            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;

            _inventoryNavigatedEvent = eventAggregator.GetEvent<InventoryNavigatedEvent>();
            regionManager.Regions[Constants.MainContentRegion].NavigationService.Navigated += NavigationService_Navigated;
        }

        #endregion //Constructor

        #region Methods
        
        void NavigationService_Navigated(Object sender, RegionNavigationEventArgs e) {
            _inventoryNavigatedEvent.Publish(e.Uri.ToString());
        }

        void RefreshData() {
            DataItems.Source = null;
            if(this.ItemIsChecked) {
                this.LoadItems();
            } else if(this.CategoryIsChecked) {
                this.LoadCategories();
            }
        }

        void AddNavigate() {
            if(this.ItemIsChecked) {
                this.NavigateRequest(Constants.MainContentRegion, typeof(ItemView).FullName, Constants.Key, Constants.AddNew);
            } else if(this.CategoryIsChecked) {
                this.NavigateRequest(Constants.MainContentRegion, typeof(CategoryView).FullName, Constants.Key, Constants.AddNew);
            }
        }

        void LoadItems() {
            _itemRepository.GetAll(result => { this.DataItems.Source = result; }, this.DisplayException);
            ActiveDataItems.Source = this.GetActiveDataItems(typeof(ItemView));
        }

        void LoadCategories() {
            _categoryRepository.GetAll(result => { this.DataItems.Source = result; }, this.DisplayException);
            ActiveDataItems.Source = this.GetActiveDataItems(typeof(CategoryView));
        }
        
        IList<NonLinearNavigationMetadata> GetActiveDataItems(Type viewType) {
            var list = new List<NonLinearNavigationMetadata>();
            foreach(var item in this.RegionManager.Regions[Constants.MainContentRegion].Views) {
                if(item.GetType() == viewType) {
                    var fwe = item as FrameworkElement;
                    if(fwe != null) {
                        var nonLinearNavigationObject = fwe.DataContext as INonLinearNavigationObject;
                        if(nonLinearNavigationObject != null) {
                            list.Add(new NonLinearNavigationMetadata(nonLinearNavigationObject));
                        }
                    }
                }
            }
            return list;
        }

        String MakeLabelWithCountForApplicationView(Type viewType, String labelText) {
            
            Int32 count = this.RegionManager.Regions[Constants.MainContentRegion].Views.Count(item => item.GetType() == viewType);

            return count == 0 ? labelText : String.Format("{0}-({1})", labelText, count);
        }

        void UpdateActiveDataItems() {
            try {
                if(ItemIsChecked)
                    ActiveDataItems.Source = this.GetActiveDataItems(typeof(ItemView));
                else if(CategoryIsChecked)
                    ActiveDataItems.Source = this.GetActiveDataItems(typeof(CategoryView));
            } catch (Exception) { }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext) {
            UpdateActiveDataItems();
            RaisePropertyChanged("ItemText");
            RaisePropertyChanged("CategoryText");
        }

        void IPartImportsSatisfiedNotification.OnImportsSatisfied() {
            this.ItemIsChecked = true;
        }

        #endregion //Methods
    }
}
