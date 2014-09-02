
using System;
using System.ComponentModel.DataAnnotations;

namespace Stuff.BusinessEntityObjects {

    /// <summary>
    /// Represents a Movie 
    /// </summary>
    [Serializable]
    public class Movie : ValuableBase {

        #region Declarations

        String _cast;
        String _mediaType;

        Double _myRating;

        Double _publicRating;
        Int32? _yearReleased;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the cast.
        /// </summary>
        /// <value>The cast.</value>
        public String Cast {
            get { return _cast; }
        }

        /// <summary>
        /// Gets or sets the type of the media.
        /// </summary>
        /// <value>The type of the media.</value>
        [Required]
        public String MediaType {
            get { return _mediaType; }
            set { base.SetPropertyValue<String>("MediaType", ref _mediaType, value); }
        }

        /// <summary>
        /// Gets or sets my rating.
        /// </summary>
        /// <value>My rating.</value>
        public Double MyRating {
            get { return _myRating; }
            set { base.SetPropertyValue<Double>("MyRating", ref _myRating, value); }
        }

        /// <summary>
        /// Gets the public rating.
        /// </summary>
        /// <value>The public rating.</value>
        public Double PublicRating {
            get { return _publicRating; }
        }

        /// <summary>
        /// Gets the year released.
        /// </summary>
        /// <value>The year released.</value>
        public Int32? YearReleased {
            get { return _yearReleased; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Movie"/> class.
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
        public Movie(String name, String description, String imageURL, Byte[] image, Double paid, String id, String purchasedFrom, String cast, Double publicRating, Int32? yearReleased, String mediaType, Double myRating)
            : base(name, description, imageURL, image, paid, id, purchasedFrom) {
            _cast = cast;
            _publicRating = publicRating;
            _yearReleased = yearReleased;
            _mediaType = mediaType;
            _myRating = myRating;
        }

        #endregion

        #region Methods

        public override bool Contains(string filterText) {

            if (base.Contains(filterText)) {
                return true;
            }

            if (Cast.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0) {
                return true;
            }
            return false;
        }

        #endregion
    }
}
