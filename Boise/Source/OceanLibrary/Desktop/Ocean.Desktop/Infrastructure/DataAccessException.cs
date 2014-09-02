
using System;

namespace Ocean.Infrastructure {

    /// <summary>
    /// Represents DataAccessException, a custom application exception class used by the data access layer for packaging all exceptions throw by the data access layer
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable")]
    public class DataAccessException : Exception {

        #region  Properties

        /// <summary>
        /// Gets the return code.
        /// </summary>
        /// <value>The return code.</value>
        public DatabaseReturnCode ReturnCode { get; private set; }

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessException"/> class.
        /// </summary>
        /// <param name="returnCode">The return code.</param>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        public DataAccessException(DatabaseReturnCode returnCode, String source, String message)
            : base(message) {
            base.Data.Add("Source", source);
            this.ReturnCode = returnCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessException"/> class.
        /// </summary>
        /// <param name="returnCode">The return code.</param>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DataAccessException(DatabaseReturnCode returnCode, String source, String message, Exception innerException)
            : base(message, innerException) {
            base.Data.Add("Source", source);
            this.ReturnCode = returnCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessException"/> class.
        /// </summary>
        /// <param name="returnCode">The return code.</param>
        /// <param name="message">The message.</param>
        public DataAccessException(DatabaseReturnCode returnCode, String message)
            : base(message) {
            this.ReturnCode = returnCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccessException"/> class.
        /// </summary>
        /// <param name="returnCode">The return code.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DataAccessException(DatabaseReturnCode returnCode, String message, Exception innerException)
            : base(message, innerException) {
            this.ReturnCode = returnCode;
        }
        #endregion
    }
}