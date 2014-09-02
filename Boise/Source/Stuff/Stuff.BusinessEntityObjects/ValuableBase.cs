
using System;
using System.ComponentModel.DataAnnotations;
using Simple.Core.BusinessEntity;

namespace Stuff.BusinessEntityObjects {

    /// <summary>
    /// Represents the ValuableBase class which is the base class for all objects stored in Stuff
    /// </summary>
    [Serializable]
    public abstract class ValuableBase : BusinessEntityBase {

        #region Declarations

        String _description;
        String _id;
        Byte[] _image;
        String _imageURL;
        String _name;

        Double _paid;
        String _purchasedFrom;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>The description.</value>
        public String Description {
            get { return _description; }
        }

        /// <summary>
        /// Gets the unique Id used by the remote data service.
        /// </summary>
        /// <value>The id.</value>
        public String Id {
            get { return _id; }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Byte[] Image {
            get { return _image; }
            set { base.SetPropertyValue<Byte[]>("Image", ref _image, value); }
        }

        /// <summary>
        /// Gets the image URL.
        /// </summary>
        /// <value>The image URL.</value>
        public String ImageURL {
            get { return _imageURL; }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public String Name {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets the amount paid.
        /// </summary>
        /// <value>The amount paid.</value>
        public Double Paid {
            get { return _paid; }
            set { base.SetPropertyValue<Double>("Paid", ref _paid, value); }
        }

        /// <summary>
        /// Gets or sets the purchased from.
        /// </summary>
        /// <value>The purchased from.</value>
        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public String PurchasedFrom {
            get { return _purchasedFrom; }
            set { base.SetPropertyValue<String>("PurchasedFrom", ref _purchasedFrom, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ValuableBase"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="imageURL">The image URL.</param>
        /// <param name="image">The image.</param>
        /// <param name="paid">The amount paid.</param>
        /// <param name="id">The unique Id used by the remote data service.</param>
        /// <param name="purchasedFrom">The purchased from.</param>
        public ValuableBase(String name, String description, String imageURL, Byte[] image, Double paid, String id, String purchasedFrom)
            : base() {
            _name = name;
            _description = description;
            _imageURL = imageURL;
            _image = image;
            _paid = paid;
            _id = id;
            _purchasedFrom = purchasedFrom;
        }

        #endregion

        #region Methods

        public virtual Boolean Contains(String filterText) {

            if (Name.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0) {
                return true;
            }
            return false;
        }

        #endregion
    }
}
