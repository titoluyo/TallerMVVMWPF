using System;
using Microsoft.Practices.Prism.Regions;
using Wpf.Common.Infrastructure;
using Wpf.Navigation.Services;
using System.Windows.Input;

namespace Wpf.Navigation.Navigation {

    public class NavigationApiDemoViewModel : ObservableObject, IRegionMemberLifetime {
        
        readonly IRegionManager _regionManager;
        ICommand _navigateCommand;

        public ICommand NavigateCommand {
            get { return _navigateCommand ?? (_navigateCommand = new RelayCommand<String>(NavigateExecute)); }
        }
	
        public NavigationApiDemoViewModel(IRegionManager regionManager) {
            _regionManager = regionManager;
            if (regionManager == null) throw new ArgumentNullException("regionManager");
        }

        void NavigateExecute(String id) {
            this.NavigateRequest(
                Constants.NavigationModuleContentRegionName,
                typeof (DataFormView).FullName,
                new[,] {{"ID", id}});
        }

        Boolean IRegionMemberLifetime.KeepAlive {
            get { return false; }
        }

        void NavigateRequest(String regionName, String target, String[,] parms) {
            _regionManager.Regions[regionName].RequestNavigate(new Uri(QueryStringBuilder.Construct(target, parms), UriKind.Relative));
        }
    }
}
