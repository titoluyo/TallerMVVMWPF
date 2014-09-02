using System;
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
    public class CategoryViewModel : FormNavigationAwareViewModelBase{

        #region Declarations

        Category _category;
        readonly ICategoryRepository _categoryRepository;

        #endregion //Declarations

        #region Properties

        public override String Application {
            get { return Constants.Inventory; }
        }
        
        public Category Category {
            get { return _category; }
            set {
                _category = value;
                this.RaisePropertyChanged("Category");
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
        public CategoryViewModel(IRegionManager regionManager, ICategoryRepository categoryRepository)
            : base(regionManager) {
            _categoryRepository = categoryRepository;
        }

        #endregion //Constructor

        #region Methods

        void CloseExecute() {
            this.SetKeepAliveFalse();
            this.RegionManager.RequestNavigate(Constants.MainContentRegion, typeof(InventoryView).FullName);
        }

        protected override void OnNavigatedTo(Int32 key) {
            _categoryRepository.Get(
                key, result => { this.Category = result; }, this.DisplayException);
        }

        protected override Boolean IsNavigationTarget(Int32 key) {
            return key == this.Category.CategoryID;
        }

        public override void OnNavigatedFrom(NavigationContext navigationContext) {
            base.OnNavigatedFrom(navigationContext);

            String state = this.Category.CategoryID == 0 ? "Adding" : "Editing";
            this.SetNonLinearNavigationObject(this.Category.Description, state, this.Category.CategoryID.ToString(), this.Application);
        }

        #endregion //Methods
    }
}
