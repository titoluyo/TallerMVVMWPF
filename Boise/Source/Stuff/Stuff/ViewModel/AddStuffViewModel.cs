
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Simple.Core.Services.Dialog;
using Simple.WPF.Infrastructure;
using Stuff.Business;
using Stuff.BusinessEntityObjects;
using Stuff.Events;

namespace Stuff.ViewModel {

    /// <summary>
    /// Represents an AddStuffViewModel
    /// </summary>
    public class AddStuffViewModel : ViewModelBase {

        #region Declarations

        /// <summary>
        /// Using BackgroundWorker because it simplifies multi-threaded data access.
        /// </summary>
        BackgroundWorker _backgroundWorker = new BackgroundWorker();
        Boolean _isSearching;
        ObservableCollection<MovieSearchResult> _movieSearchResult;
        String _searchText;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is searching.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is searching; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsSearching {
            get { return _isSearching; }

            private set {
                _isSearching = value;
                RaisePropertyChanged("IsSearching");
            }
        }

        /// <summary>
        /// Gets the movie search results.
        /// </summary>
        /// <value>The movie search results.</value>
        public ObservableCollection<MovieSearchResult> MovieSearchResults {
            get { return _movieSearchResult; }

            private set {
                _movieSearchResult = value;
                RaisePropertyChanged("MovieSearchResults");
            }
        }
        IRemoteDataStore RemoteDataStore {
            get { return base.GetService<IRemoteDataStore>(); }
        }

        /// <summary>
        /// Gets or sets the search text.
        /// </summary>
        /// <value>The search text.</value>
        public String SearchText {
            get { return _searchText; }
            set {
                _searchText = value;
                RaisePropertyChanged("SearchText");
            }
        }

        #endregion

        #region Command Properties

        /// <summary>
        /// Gets the AddMovieCommand.
        /// </summary>
        public ICommand AddMovieCommand { get { return new RelayCommand<MovieSearchResult>(AddMovieExecute, (r) => r != null && r.HasNoBrokenValidationRules); } }

        /// <summary>
        /// Gets the SearchByTitleCommand
        /// </summary>
        public ICommand SearchByTitleCommand { get { return new RelayCommand<Object>(SearchByTitleExecute); } }

        /// <summary>
        /// Gets the ShowBrowseStuffViewCommand
        /// </summary>
        public ICommand ShowBrowseStuffViewCommand {
            get {
                return new RelayCommand(() => {
                    base.GetEvent<ShowBrowseStuffViewEvent>().Publish(null);
                    MovieSearchResults = null;
                    SearchText = String.Empty;
                });
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddStuffViewModel"/> class.
        /// </summary>
        public AddStuffViewModel() {
            _backgroundWorker.DoWork += new DoWorkEventHandler(_backgroundWorker_DoWork);
            _backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_backgroundWorker_RunWorkerCompleted);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Do all the heavy lifting on the background thread.  
        /// Request the data, sort it and force LINQ to exectute the query.
        /// Using the parallel libraries, for each returned object perform the following: 
        ///     run the validation checks for each object, 
        ///     and make each data movie if its already in our database.
        /// </summary>
        void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e) {
            //ToList forces the LINQ query to execute here.
            //We need the query to not perform delayed execution so that the paralled methods will run correctly.
            var results = RemoteDataStore.SearchByName(e.Argument.ToString()).OrderBy(r => r.Name).ToList();
            //Read up on PLINQ and Parallel and why to use Parallel here.
            //http://services.social.microsoft.com/feeds/FeedItem?feedId=639a99a9-ff25-4062-b61d-a86ea9d66a06&itemId=a611e616-779e-4bb9-b88f-8ab228165a82&title=When+Should+I+Use+Parallel.ForEach%3f+When+Should+I+Use+PLINQ%3f&uri=http%3a%2f%2fdownload.microsoft.com%2fdownload%2fB%2fC%2fF%2fBCFD4868-1354-45E3-B71B-B851CD78733D%2fWhenToUseParallelForEachOrPLINQ.pdf&k=gAdKnW7xH8RXzlALrQceTdMvpCzxVujB07sMnqbwvgM%3d
            //
            results.AsParallel().ForAll(r => { r.IsValid(); r.InLocalDatabase = MovieDataStoreService.Keys.Contains(r.Id); });
            e.Result = results;
        }

        /// <summary>
        /// Runs after the backgroundworker completes the work in the DoWork method
        /// </summary>
        void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            IsSearching = false;

            if (e.Error != null) {
                Dialog.ShowMessage(e.Error.ToString(), "Error While Searching", DialogButton.OK, DialogImage.Error);
            } else {
                MovieSearchResults = new ObservableCollection<MovieSearchResult>((IEnumerable<MovieSearchResult>)e.Result);
            }
        }

        void AddMovieExecute(MovieSearchResult movieSearchResult) {

            if (!movieSearchResult.IsValid()) {
                Dialog.ShowMessage(String.Format("Movie is invalid.  Errors {0}", movieSearchResult.Error), "Invalid Data", DialogButton.OK, DialogImage.Exclamation);
                return;
            }
            Movie movie = new Movie(
                                     movieSearchResult.Name,
                                     movieSearchResult.Description,
                                     movieSearchResult.ImageURL,
                                     movieSearchResult.Image,
                                     movieSearchResult.Paid,
                                     movieSearchResult.Id,
                                     movieSearchResult.PurchasedFrom,
                                     movieSearchResult.Cast,
                                     movieSearchResult.PublicRating,
                                     movieSearchResult.YearReleased,
                                     movieSearchResult.MediaType,
                                     movieSearchResult.MyRating);
            MovieDataStoreService.Items.Add(movie);
            movieSearchResult.InLocalDatabase = true;
            MovieDataStoreService.Save();
        }

        void SearchByTitleExecute(Object param) {

            if (!_backgroundWorker.IsBusy) {
                IsSearching = true;
                MovieSearchResults = null;
                _backgroundWorker.RunWorkerAsync(SearchText);
            }
        }

        #endregion
    }
}
