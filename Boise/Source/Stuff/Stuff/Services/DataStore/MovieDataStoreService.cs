
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Simple.Core.Services.Container;
using Simple.Core.Services.Dialog;
using Stuff.BusinessEntityObjects;

namespace Stuff.Services.DataStore {

    /// <summary>
    /// Represents a singleton MovieDataStore
    /// </summary>
    [Serializable()]
    public class MovieDataStoreService : Stuff.Services.DataStore.IMovieDataStoreService {

        #region Declarations

        readonly String _dataStoreFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Stuff Data", "Movie.bin");
        readonly String _dataStoreFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Stuff Data");
        ObservableCollection<Movie> _items;
        [field: NonSerialized]
        Hashtable _keys;

        #endregion

        #region Properties

        /// <summary>
        /// Provides access to the ServiceContainer singleton.
        /// </summary>
        public static readonly MovieDataStoreService Instance = new MovieDataStoreService();

        /// <summary>
        /// Gets the item count.
        /// </summary>
        /// <value>The count.</value>
        public Int32 Count { get { return _items.Count; } }
        IDialogService Dialog {
            get { return ServiceContainer.Instance.GetService<IDialogService>(); }
        }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public ObservableCollection<Movie> Items {
            get {

                if (_items == null)
                    _items = new ObservableCollection<Movie>();
                return _items;
            }
        }

        /// <summary>
        /// Gets the keys.
        /// </summary>
        /// <value>The keys.</value>
        /// <remarks>
        /// The Hastable is a thread safe container for reading.  This feature enables Parallel lookups for a set of data.
        /// These are not serialized to conservere space.  Instead this Hashtable is repopulated automatically after deserialization.
        /// </remarks>
        public Hashtable Keys {
            get {

                if (_keys == null)
                    _keys = new Hashtable();
                return _keys;
            }
        }

        #endregion

        #region Constructor

        MovieDataStoreService() {
            Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Items_CollectionChanged);
        }

        #endregion

        #region Methods

        void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add) {

                foreach (var item in e.NewItems) {
                    var m = item as Movie;

                    if (m != null)
                        Keys.Add(m.Id, null);
                }
            } else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove) {

                foreach (var item in e.OldItems) {
                    var m = item as Movie;

                    if (m != null)
                        Keys.Remove(m.Id);
                }
            } else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset) {
                Keys.Clear();

                foreach (var item in Items) {
                    var m = item as Movie;

                    if (m != null)
                        Keys.Add(m.Id, null);
                }
            }
        }

        #endregion

        #region Load and Save

        ObservableCollection<Movie> Deserialize(System.IO.Stream fs) {
            var binaryFormatter = new BinaryFormatter();
            ObservableCollection<Movie> items = null;

            try {
                //this always works in VS (because VS recycles the app domain on build) and the first time in Blend
                items = (ObservableCollection<Movie>)binaryFormatter.Deserialize(fs);
            } catch (System.InvalidCastException) {
                //this gets run when Blend blows up on second and subsequent reloads
                fs.Position = 0;
                binaryFormatter = new BinaryFormatter();
                //this is the magic right here
                binaryFormatter.Binder = new LookUpTypeObjectBinder();
                items = (ObservableCollection<Movie>)binaryFormatter.Deserialize(fs);
            } catch (Exception ex) {
                throw ex;
            }
            return items;
        }

        /// <summary>
        /// Loads the Items from a binary file.
        /// </summary>
        public Boolean Load() {

            if (!VerifyDataStoreFolder())
                return false;
            Boolean returnValue = true;

            try {

                if (File.Exists(_dataStoreFileName)) {

                    using (Stream fs = new FileStream(_dataStoreFileName, FileMode.Open)) {

                        //HACK - Had to change this code to handle Blend 3/4 not recycling their app domain
                        //Without this, the second time you attempt to reload the project with the Browsing View visible, deserializing will blow up.
                        //
                        //var binaryFormatter = new BinaryFormatter();
                        //_items = (ObservableCollection<Movie>)binaryFormatter.Deserialize(fs);
                        //
                        _items = this.Deserialize(fs);
                    }
                    //Since Blend keeps this data around at design-time, need to clear the keys.
                    //The point is, don't make assumptions in any code that could possibly be run at design-time.
                    //Program design-time code with zero assumptions.
                    Keys.Clear();

                    foreach (var item in Items) {
                        Keys.Add(item.Id, null);
                    }
                }
            } catch (Exception ex) {
                returnValue = false;
                Dialog.ShowException(String.Format("Bummer, error deserializing the data store file.  Close program and investiage.{0}{0}Error message: {1}", Environment.NewLine, ex.ToString()));
            }
            return returnValue;
        }

        /// <summary>
        /// Saves the Items to a binary file.
        /// </summary>
        public Boolean Save() {

            if (!VerifyDataStoreFolder())
                return false;
            Boolean returnValue = true;

            try {

                using (System.IO.FileStream fs = new System.IO.FileStream(_dataStoreFileName, System.IO.FileMode.Create)) {

                    var binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(fs, _items);
                }
            } catch (Exception ex) {
                returnValue = false;
                Dialog.ShowException(String.Format("Bummer, error saving the data store file.  Close program and investiage.{0}{0}Error message: {1}", Environment.NewLine, ex.ToString()));
            }
            return returnValue;
        }
        Boolean VerifyDataStoreFolder() {
            Boolean returnValue = true;

            try {

                if (!Directory.Exists(_dataStoreFolder)) {
                    Directory.CreateDirectory(_dataStoreFolder);
                }
            } catch (Exception ex) {
                returnValue = false;
                Dialog.ShowException(String.Format("Bummer, error creating the folder: {0}{1}{1}{2}", _dataStoreFolder, Environment.NewLine, ex.ToString()));
            }
            return returnValue;
        }

        internal sealed class LookUpTypeObjectBinder : SerializationBinder {

            public override Type BindToType(String assemblyName, String typeName) {
                Type typeToDeserialize = null;
                typeToDeserialize = Type.GetType(String.Format("{0}, {1}", typeName, assemblyName));
                return typeToDeserialize;
            }
        }

        #endregion
    }
}
