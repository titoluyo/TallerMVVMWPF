
using System;
using System.Collections.Generic;
using Stuff.BusinessEntityObjects;

namespace Stuff.Business {

    /// <summary>
    /// A contract for IRemoteDataStore
    /// </summary>
    public interface IRemoteDataStore {
        /// <summary>
        /// Searches the remote data store by the name of the valuable.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of valuable to return in the <c>SearchResult.Item</c> property.</typeparam>
        /// <param name="name">The name to search for.</param>
        /// <returns>IEnumerable&lt;SearchResult&lt;T&&gt;&gt; from the remote data store that contain the name.</returns>
        IEnumerable<MovieSearchResult> SearchByName(String name);
    }
}
