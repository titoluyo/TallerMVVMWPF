
using System;

namespace Simple.Core.BusinessEntity {

    /// <summary>
    /// Represents a broken validation rule
    /// </summary>
    public class BrokenRule {

        #region Properties

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public String ErrorMessage { get; private set; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <value>The name of the property.</value>
        public String PropertyName { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="BrokenRule"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="errorMessage">The error message.</param>
        public BrokenRule(String propertyName, String errorMessage) {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
        }

        #endregion
    }
}
