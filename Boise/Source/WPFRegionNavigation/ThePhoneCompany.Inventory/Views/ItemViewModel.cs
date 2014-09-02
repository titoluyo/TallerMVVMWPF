using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using ThePhoneCompany.Common.Commands;
using ThePhoneCompany.Common.Constants;
using ThePhoneCompany.Common.Infrastructure;
using ThePhoneCompany.Inventory.Business;
using ThePhoneCompany.Inventory.ThePhoneCompanyDataService;

namespace ThePhoneCompany.Inventory.Views {

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ItemViewModel : FormNavigationAwareViewModelBase {

        #region Declarations

        Item _item;
        ObservableCollection<Category> _categories;
        readonly IItemRepository _itemRepository;
        readonly ICategoryRepository _categoryRepository;

        #endregion //Declarations

        #region Properties

        public override String Application {
            get { return Constants.Inventory; }
        }


        public Item Item {
            get { return _item; }
            set {
                _item = value;
                this.RaisePropertyChanged("Item");
            }
        }

        public ObservableCollection<Category> Categories {
            get { return _categories; }
            private set { 
                _categories=value;
                this.RaisePropertyChanged("Categories");
            }
        }

        #endregion //Properties

        #region Command Properties

        public ICommand CloseCommand {
            get { return new RelayCommand(CloseExecute); }
        }

        #endregion //Command Properties

        #region Constructor

        [ImportingConstructor]
        public ItemViewModel(IRegionManager regionManager, IItemRepository itemRepository, ICategoryRepository categoryRepository)
            : base(regionManager) {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
        }

        #endregion //Constructor

        #region Methods

        void CloseExecute() {
            this.SetKeepAliveFalse();
            this.RegionManager.RequestNavigate(Constants.MainContentRegion, typeof(InventoryView).FullName);
        }

        protected override void OnNavigatedTo(Int32 key) {
            _categoryRepository.GetAll(
                result => { this.Categories = new ObservableCollection<Category>(result); }, this.DisplayException);
            
            _itemRepository.Get(
                key, result => { this.Item = result; }, this.DisplayException);
        }

        protected override Boolean IsNavigationTarget(Int32 key) {
            return key == this.Item.ItemID;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) {
            base.OnNavigatedFrom(navigationContext);

            String state = this.Item.ItemID == 0 ? "Adding" : "Editing";
            this.SetNonLinearNavigationObject(this.Item.Description, state, this.Item.ItemID.ToString(), this.Application);
        }
        
        #endregion //Methods
    }
}
