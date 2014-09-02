using System;
using System.Collections.Generic;
using Ocean.Infrastructure;

namespace Ocean.Audit {

    /// <summary>
    /// Represents IBusinessEntityAudit contract
    /// </summary>
    public interface IBusinessEntityAudit {

        /// <summary>
        /// Creates an IDictionary for class properties and values, where class properites are decorated with the <see cref="AuditAttribute"/>
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns></returns>
        IDictionary<String, String> ToAuditIDictionary(String defaultValue, IDictionary<String, String> dictionary);
        
        /// <summary>
        /// Creates an delimitated String class properties and values, where class properites are decorated with the <see cref="AuditAttribute"/>
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns></returns>
        String ToAuditString(String defaultValue, String delimiter = GlobalConstants.STRING_DEFAULT_DELIMITER);

        /// <summary>
        /// Creates an IDictionary for class properties and values
        /// </summary>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="sortByPropertyName">Name of the sort by property.</param>
        /// <returns></returns>
        IDictionary<String, String> ToClassIDictionary(String defaultValue, IDictionary<String, String> dictionary, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes);

        /// <summary>
        /// Creates an delimitated String class properties and values
        /// </summary>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="sortByProperytName">if set to <c>true</c> [sort by properyt name].</param>
        /// <returns></returns>
        String ToClassString(String delimiter = GlobalConstants.STRING_DEFAULT_DELIMITER, SortByPropertyName sortByProperytName = SortByPropertyName.Yes);
    }
}

