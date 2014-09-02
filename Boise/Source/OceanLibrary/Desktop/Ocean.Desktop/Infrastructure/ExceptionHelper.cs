using System;

namespace Ocean.Infrastructure {

    /// <summary>
    /// Represents the Exception Helper
    /// </summary>
    public class ExceptionHelper {

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHelper"/> class.
        /// </summary>
        ExceptionHelper() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Gets the lowest inner exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static Exception GetLowestInnerException(Exception ex) {

            if(ex.InnerException == null) {
                return ex;
            }

            Exception exInner = ex.InnerException;

            do {

                if(exInner.InnerException != null) {
                    exInner = exInner.InnerException;
                } else {
                    return exInner;
                }

            } while(true);
        }

        #endregion
    }
}

