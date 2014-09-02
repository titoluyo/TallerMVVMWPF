
using System;
using System.Collections.Generic;
using System.Linq;
using Stuff.BusinessEntityObjects;

namespace Stuff.Business {

    /// <summary>
    /// Represents a NetflixRemoteDataStore.  This class is a wrapper to the Netflix oData.
    /// </summary>
    public class NetflixRemoteDataStore : IRemoteDataStore {

        #region Declarations

        readonly String NETFLIX_CATALOG_URI = "http://odata.netflix.com/Catalog/";

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NetflixRemoteDataStore"/> class.
        /// </summary>
        public NetflixRemoteDataStore() { }

        #endregion

        #region Methods

        String EnsureImageURL(String url) {

            if (String.IsNullOrWhiteSpace(url))
                return "/Stuff;component/Images/dummyfiller.png";

            else
                return url;
        }

        /// <summary>
        /// Searches the Netflix remote data store by the name of the valuable.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of valuable to return in the <c>SearchResult.Item</c> property.</typeparam>
        /// <param name="name">The name to search for.</param>
        /// <returns>IEnumerable&lt;SearchResult&lt;T&&gt;&gt; from the Netflix remote data store that contain the name.</returns>
        public IEnumerable<MovieSearchResult> SearchByName(String name) {
            var catalog = new Netflix.Data.NetflixServiceReference.NetflixCatalog(new Uri(NETFLIX_CATALOG_URI));
            //I wrote this code this way, instead of using .Expand("Cast") so that I could create an anonymous type.
            //Using .Expand prohibits anonymous types.
            //Using anonymous type brings back only the data I need from the server
            var titles = from t in catalog.Titles
                         where t.Name.Contains(name)
                         select new { t.Name, t.Synopsis, t.ReleaseYear, t.AverageRating, t.BoxArt, t.Cast, t.Id };

            foreach (var t in titles) {
                yield return new MovieSearchResult(t.Name, StripHTML(t.Synopsis), EnsureImageURL(t.BoxArt.LargeUrl), null, 0, t.Id, String.Empty, String.Join(@", ", t.Cast.OrderBy(c => c.Name).Select(c => c.Name).ToArray()), t.AverageRating == null ? 0.0 : t.AverageRating.Value, t.ReleaseYear, String.Empty, 0.0);
            }
        }
        /// <summary>
        /// Cleans out the HTML from the string.  Netflix puts HTML in their Synopsis, lets remove it.
        /// They also sometimes wrap the HTML in (  ) so let's remove that too.
        /// </summary>
        String StripHTML(String source) {
            Char[] array = new Char[source.Length];
            Int32 arrayIndex = 0;
            Boolean inside = false;

            for (Int32 i = 0; i < source.Length; i++) {
                Char c = source[i];

                if (c == '<') {
                    inside = true;
                    continue;
                }

                if (c == '>') {
                    inside = false;
                    continue;
                }

                if (!inside) {

                    if (c != '(' && c != ')') {
                        //character is not HTML or ( or )
                        array[arrayIndex] = c;
                        arrayIndex++;
                    }
                }
            }
            return new String(array, 0, arrayIndex);
        }

        #endregion
    }
}
