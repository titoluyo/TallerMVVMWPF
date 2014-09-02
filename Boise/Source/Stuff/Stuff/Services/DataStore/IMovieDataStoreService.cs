
using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace Stuff.Services.DataStore {

    /// <summary>
    /// Represents the IMovieDataStoreService contract
    /// </summary>
    public interface IMovieDataStoreService {
        Boolean Load();
        Boolean Save();
        Int32 Count { get; }
        ObservableCollection<Stuff.BusinessEntityObjects.Movie> Items { get; }
        Hashtable Keys { get; }
    }
}
