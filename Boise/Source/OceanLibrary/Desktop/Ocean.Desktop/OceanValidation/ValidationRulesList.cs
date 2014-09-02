using System.Collections.Generic;

namespace Ocean.OceanValidation {

    /// <summary>
    /// Represents ValidationRulesList
    /// </summary>
    public class ValidationRulesList {

        #region  Declarations

        List<IValidationRuleMethod> _list;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <value>The list.</value>
        public List<IValidationRuleMethod> List {
            get { return _list ?? (_list = new List<IValidationRuleMethod>()); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRulesList"/> class.
        /// </summary>
        public ValidationRulesList() { }

        #endregion
    }
}

