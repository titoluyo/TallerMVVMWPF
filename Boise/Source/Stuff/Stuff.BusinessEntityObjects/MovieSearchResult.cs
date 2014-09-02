
using System;
using System.ComponentModel;
using Simple.Core.BusinessEntity;
using System.ComponentModel.DataAnnotations;

namespace Stuff.BusinessEntityObjects {

    /// <summary>
    /// Represents a MovieSearchResult
    /// </summary>
    public class MovieSearchResult : Movie {

        #region Declarations

        String _amountPaid;
        Boolean _inLocalDatabase;
        Boolean _isSelected;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the amount paid.
        /// </summary>
        /// <value>The amount paid.</value>
        /// <remarks>
        /// This is an example of exposing a string property to mitigate the type mismatch errors when binding a 
        /// TextBox to a Double field and the users types an invalid character.  This type of coding won't scale, 
        /// especially if you Code Gen all your business object, but for this simple scenario works great.
        /// </remarks>
        [Required(AllowEmptyStrings = false)]
        public String AmountPaid {
            get { return _amountPaid; }
            set {
                base.SetPropertyValue<String>("AmountPaid", ref _amountPaid, value);

                Double result;

                if (Double.TryParse(value, out result)) {
                    Paid = result;
                    _amountPaid = Paid.ToString("c");
                } else {
                    BrokenRules.Add(new BrokenRule("AmountPaid", "Must enter a numeric money value"));
                }
                RaisePropertyChanged("AmountPaid");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this video is in local database or not.
        /// </summary>
        /// <value><c>true</c> if this video is in local database; otherwise, <c>false</c>.</value>
        public Boolean InLocalDatabase {
            get { return _inLocalDatabase; }
            set {
                _inLocalDatabase = value;
                RaisePropertyChanged("InLocalDatabase");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public Boolean IsSelected {
            get { return _isSelected; }
            set {
                _isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieSearchResult"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="imageURL">The image URL.</param>
        /// <param name="image">The image.</param>
        /// <param name="paid">The amount I paid.</param>
        /// <param name="id">The id supplied by the online database.</param>
        /// <param name="purchasedFrom">The store or web site I purchased this video from.</param>
        /// <param name="cast">The cast.</param>
        /// <param name="publicRating">The public's rating.</param>
        /// <param name="yearReleased">The year released.</param>
        /// <param name="mediaType">Type of the media.</param>
        /// <param name="myRating">My rating.</param>
        public MovieSearchResult(String name, String description, String imageURL, Byte[] image, Double paid, String id, String purchasedFrom, String cast, Double publicRating, Int32? yearReleased, String mediaType, Double myRating)
            : base(name, description, imageURL, image, paid, id, purchasedFrom, cast, publicRating, yearReleased, mediaType, myRating) {
        }

        #endregion
    }
}
