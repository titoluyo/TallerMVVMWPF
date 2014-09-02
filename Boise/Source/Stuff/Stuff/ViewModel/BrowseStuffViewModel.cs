
using System;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Simple.WPF.Infrastructure;
using Stuff.BusinessEntityObjects;
using Stuff.Events;

namespace Stuff.ViewModel {

    /// <summary>
    /// Represents the BrowseStuffViewModel
    /// </summary>
    public class BrowseStuffViewModel : ViewModelBase {

        #region Declarations

        String _filterText;
        ListCollectionView _searchResults;
        ICommand _showEditStuffViewCommand;
        delegate void FilterDelegate();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the filter text.  When set, initiates a search operation.
        /// </summary>
        /// <value>The filter text.</value>
        public String FilterText {
            get { return _filterText; }
            set {
                _filterText = value;
                RaisePropertyChanged("FilterText");
                Search();
            }
        }

        /// <summary>
        /// Gets the search results.
        /// </summary>
        /// <value>The search results.</value>
        public ListCollectionView SearchResults {
            get { return _searchResults; }
        }

        #endregion

        #region Command Properties

        /// <summary>
        /// Gets the ShowAddStuffView command.
        /// </summary>
        /// <value>The show add stuff view command.</value>
        public ICommand ShowAddStuffViewCommand {
            get { return new RelayCommand(() => GetEvent<ShowAddStuffViewEvent>().Publish(null)); }
        }

        /// <summary>
        /// Gets the ShowEditStuffView command.
        /// </summary>
        /// <value>The show edit stuff view command.</value>
        public ICommand ShowEditStuffViewCommand {
            get {

                if (_showEditStuffViewCommand == null)
                    _showEditStuffViewCommand = new RelayCommand<Movie>(ShowEditStuffViewExecute);
                return _showEditStuffViewCommand;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BrowseStuffViewModel"/> class.
        /// </summary>
        public BrowseStuffViewModel() {
            MovieDataStoreService.Load();
            _searchResults = new ListCollectionView(MovieDataStoreService.Items);
            _searchResults.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        #endregion

        #region Command Methods

        void ShowEditStuffViewExecute(Movie movie) {
            GetEvent<ShowEditStuffViewEvent>().Publish(movie);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the Filter property on the SearchResults ListCollectionView.
        /// If the FilterText is an empty string it will clear the Filter property.
        /// If the FilterText is not an empty string, it creates a new Predicate that passes the 
        /// FilterText to each Movie's Contains method that performs the search and returns a Boolean result.
        /// </summary>
        void Search() {
            _searchResults.Dispatcher.BeginInvoke(
                (Action)delegate {

                if (String.IsNullOrWhiteSpace(FilterText))
                    _searchResults.Filter = null;

                else
                    _searchResults.Filter = new Predicate<Object>(o => ((Movie)o).Contains(FilterText));
            }, System.Windows.Threading.DispatcherPriority.ApplicationIdle);
        }

        #endregion
    }
}
