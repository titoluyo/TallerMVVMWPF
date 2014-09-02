using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using WPFPrismv4Navigation.Constants;
using WPFPrismv4Navigation.Infrastructure;

namespace WPFPrismv4Navigation.UI {

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ShellViewModel : ObservableObject, IPartImportsSatisfiedNotification {
        
        readonly Logger _logger;
        readonly IRegionManager _regionManager;
        String _dataFilterSelectedItem;

        public CollectionViewSource LogItems { get; private set; }
        public ObservableCollection<INonLinearNavigationObject> InstantiatedViews { get; private set; }
        public ObservableCollection<String> DataFilterOptions { get; private set; }

        public String DataFilterSelectedItem {
            get { return _dataFilterSelectedItem; }
            set {
                _dataFilterSelectedItem = value;
                this.RaisePropertyChanged(() => DataFilterSelectedItem);
                this.SelectLogItemsFilter(_dataFilterSelectedItem);
            }
        }

        public ICommand BeginNavigationTracingCommand { get { return new DelegateCommand(BeginNavigationTracingExecute); } }
        public ICommand NavigateCommand { get { return new DelegateCommand<String>(NavigateExecute); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="logger">The logger.</param>
        [ImportingConstructor]
        public ShellViewModel(IRegionManager regionManager, Logger logger) {
            _regionManager = regionManager;
            _logger = logger;
            //wire up the Regions.CollectionChanged event
            _regionManager.Regions.CollectionChanged += Regions_CollectionChanged;
        }

        /// <summary>
        /// Selects the log items filter to apply.
        /// </summary>
        /// <param name="value">The value used to select the filter to apply.</param>
        void SelectLogItemsFilter(String value) {
            switch(value) {
                case Global.ShowAll:
                    this.LogItems.View.Filter = null;
                    break;
                case Global.ShowOnlyViewData:
                    //HACK - How to pass more than one paramenter to a Predicate<Object> with using a module level variable.
                    //       The Predicate is forward to a Lambda that calls a method that takes more than one argument.
                    this.LogItems.View.Filter = new Predicate<Object>(o => FilterLogItems(o, Global.View));
                    break;
                case Global.ShowOnlyViewModelData:
                    this.LogItems.View.Filter = new Predicate<Object>(o => FilterLogItems(o, Global.ViewModel));
                    break;
            }
        }

        /// <summary>
        /// Filters the log items based on <c>endsWithFilterString</c> argument
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="endsWithFilterString">The ends with filter string.</param>
        Boolean FilterLogItems(Object item, String endsWithFilterString) {
            var logItem = item as LogItem;
            if(logItem != null) {
                return logItem.TypeName.EndsWith(endsWithFilterString);
            }
            return true;
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Regions control.
        /// The reason for doing this is to wire up to the MainContentRegion Navigated event.
        /// Since the ViewModel is actually constructed before the MainContentRegion, we listen
        /// to this event, then when the MainContentRegion is added, we can unhook this event
        /// and hook into the MainContentRegion Navigated event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Collections.Specialized.NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        void Regions_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if(e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {
                var list = e.NewItems;
                foreach(var item in list) {
                    var region = item as Region;
                    if (region == null || region.Name != RegionNames.MainContentRegion) continue;
                    _regionManager.Regions.CollectionChanged -= Regions_CollectionChanged;
                    _regionManager.Regions[RegionNames.MainContentRegion].NavigationService.Navigated += NavigationService_Navigated;
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the Navigated event of the NavigationService control.
        /// This method populates the non-linear navigation collection each time the region is navigated.
        /// Notice how the <see cref="INonLinearNavigationObject"/> is consumed below.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.Prism.Regions.RegionNavigationEventArgs"/> instance containing the event data.</param>
        void NavigationService_Navigated(object sender, RegionNavigationEventArgs e) {
            _logger.Log(Global.RegionNavigatedTo + e.Uri);
            this.InstantiatedViews.Clear();
            foreach(var item in _regionManager.Regions[RegionNames.MainContentRegion].Views) {
                var nonLinearNavigationObject = item as INonLinearNavigationObject;
                if(nonLinearNavigationObject != null) {
                    this.InstantiatedViews.Add(new NonLinearNavigationMetadata(nonLinearNavigationObject));
                } else {
                    var fwe = item as FrameworkElement;
                    if(fwe != null) {
                        nonLinearNavigationObject = fwe.DataContext as INonLinearNavigationObject;
                        if(nonLinearNavigationObject != null) {
                            this.InstantiatedViews.Add(new NonLinearNavigationMetadata(nonLinearNavigationObject));
                        }       
                    }
                }
            }
        }

        /// <summary>
        /// This method is the starting point for viewing the navigation pipeline.
        /// Each time the Navigate button is clicked, this method resets the application by removing
        /// all items from the region, resetting the Logger and initiating navigation to the ApplicationMenuView.
        /// </summary>
        void BeginNavigationTracingExecute() {

            var items = _regionManager.Regions[RegionNames.MainContentRegion].Views.ToList();

            foreach(var item in items) {
                _regionManager.Regions[RegionNames.MainContentRegion].Remove(item);
            }

            _logger.Reset();
            _logger.Log(Logger.Scenario.Navagate, Global.NavigationInitiated);
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, ViewNames.ApplicationMenuView, Callback);
        }

        /// <summary>
        /// Navigates the execute.
        /// 
        /// This command is executed by the one of the <see cref="InstantiatedViews"/> items.
        /// This is how non-visible objects can be navigated to using non-linear navigation.
        /// </summary>
        /// <param name="uri">The URI.</param>
        void NavigateExecute(String uri) {
            _logger.Log(Logger.Scenario.NavigateUsingLink, Global.NavigationInitiated);
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, new Uri(uri, UriKind.Relative), Callback);
        }

        /// <summary>
        /// Callbacks the specified result.
        /// </summary>
        /// <param name="result">The result.</param>
        void Callback(NavigationResult result) {
            _logger.Log(Global.NavigationRequestResultsCallBack + NavigationResultParser.Parse(result));
        }

        /// <summary>
        /// Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        void IPartImportsSatisfiedNotification.OnImportsSatisfied() {
            this.InstantiatedViews = new ObservableCollection<INonLinearNavigationObject>();

            this.LogItems = new CollectionViewSource {Source = _logger.Items};

            this.DataFilterOptions = new ObservableCollection<String> { Global.ShowAll, Global.ShowOnlyViewData, Global.ShowOnlyViewModelData };
            this.DataFilterSelectedItem = Global.ShowAll;
        }
    }
}
