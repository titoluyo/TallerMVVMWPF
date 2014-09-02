
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stuff.BusinessEntityObjects;
using Stuff.Events;
using System.Windows.Input;
using Simple.WPF.Infrastructure;

namespace Stuff.ViewModel {

    /// <summary>
    /// Represents the EditStuffViewModel
    /// </summary>
    public class EditStuffViewModel : ViewModelBase {

        #region Declarations

        Movie _movie;
        Movie _orginalMovie;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the movie.
        /// </summary>
        /// <value>The movie.</value>
        /// <remarks>This Movie is actually a Deep Copy of the Movie</remarks>
        public Movie Movie {
            get { return _movie; }
            set {
                _movie = value;
                RaisePropertyChanged("Movie");
            }
        }

        #endregion

        #region Command Properties

        /// <summary>
        /// Gets the close command.
        /// </summary>
        public ICommand CloseCommand {
            get {
                return new RelayCommand(CloseExecute);
            }
        }

        /// <summary>
        /// Gets the save command.
        /// </summary>
        public ICommand SaveCommand {
            get { return new RelayCommand(SaveExecute); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EditStuffViewModel"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor sets up an Action that will be run when the ShowEditStuffViewEvent is published.
        /// The Action saves the origianal Movie and makes a deep copy of the movie to work with.
        /// We need to do this because all movies are in memory, so any changes we make would be immediately persisted.
        /// By making a deep copy to work with, the user can edit this deep copy, when done pressing the Save button will copy the 
        /// changes on the Deep Copy to the origianal and then persite the changes to disk.
        /// 
        /// If I was editing many fields, I would have Movie implement IEditableObject.
        /// </remarks>
        public EditStuffViewModel() {
            var showEditStuffViewEvent = GetEvent<ShowEditStuffViewEvent>();
            showEditStuffViewEvent.Subscribe((movie) => { _orginalMovie = (Movie)movie; Movie = DeepCopy.Make<Movie>((Movie)movie); });
        }

        #endregion

        #region Methods

        void CloseExecute() {
            base.GetEvent<ShowBrowseStuffViewEvent>().Publish(null);
        }

        void SaveExecute() {
            _orginalMovie.MyRating = Movie.MyRating;
            MovieDataStoreService.Save();
            CloseExecute();
        }

        #endregion
    }
}
