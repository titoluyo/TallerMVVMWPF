
using System;

namespace Ocean.SqlCe {

    /// <summary>
    /// Represents SqlConstants
    /// </summary>
    public static class SqlConstants {

        /// <summary>
        /// Initializes the <see cref="SqlConstants"/> class.
        /// </summary>
        static SqlConstants() { }

        /// <summary>
        /// Property name ActiveRuleSet
        /// </summary>
        public const String STRING_ACTIVERULESET = "ActiveRuleSet";

        /// <summary>
        /// Property name BeginLoading
        /// </summary>
        public const String STRING_BEGINLOADING = "BeginLoading";

        /// <summary>
        /// Property name EndLoading
        /// </summary>
        public const String STRING_ENDLOADING = "EndLoading";

        /// <summary>
        /// Sql parameter @RETURN_CODE
        /// </summary>
        public const String STRING_RETURN_CODE = "@RETURN_CODE";

        /// <summary>
        /// Sql parameter @RETURN_VALUE
        /// </summary>
        public const String STRING_RETURN_VALUE = "@RETURN_VALUE";
    }
}