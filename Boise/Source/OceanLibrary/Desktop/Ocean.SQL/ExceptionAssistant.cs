using System;
using System.Collections;
using System.Data.SqlClient;
using System.Text;

namespace Ocean.Sql {

    /// <summary>
    /// Represents ExceptionAssistant
    /// </summary>
    public static class ExceptionAssistant {

        /// <summary>
        /// Initializes the <see cref="ExceptionAssistant"/> class.
        /// </summary>
        static ExceptionAssistant() { }

        /// <summary>
        /// Parses the SQL exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>String with exception properties and values separted by a NewLine</returns>
        public static String ParseSqlException(SqlException ex) {
            return ParseSqlException(ex, Environment.NewLine);
        }

        /// <summary>
        /// Parses the SQL exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="newLineCharacter">The new line character.</param>
        /// <returns>String with exception properties and values separted by a newLineCharacter</returns>
        public static string ParseSqlException(SqlException ex, String newLineCharacter) {

            var sb = new StringBuilder(1024);
            sb.Append(newLineCharacter);
            sb.Append(newLineCharacter);

            for(Int32 i = 0; i < ex.Errors.Count; i++) {
                sb.AppendFormat("Index #{0}{1}", i, newLineCharacter);
                sb.AppendFormat("Server: {0}{1}", ex.Errors[i].Server, newLineCharacter);
                sb.AppendFormat("Error Number: {0}{1}", ex.Errors[i].Number, newLineCharacter);
                sb.AppendFormat("Message: {0}{1}", ex.Errors[i].Message, newLineCharacter);
                sb.AppendFormat("Severity: {0}{1}", ex.Errors[i].Class, newLineCharacter);
                sb.AppendFormat("State: {0}{1}", ex.Errors[i].State, newLineCharacter);
                sb.AppendFormat("Source: {0}{1}", ex.Errors[i].Source, newLineCharacter);
                sb.AppendFormat("Procedure: {0}{1}", ex.Errors[i].Procedure, newLineCharacter);
                sb.AppendFormat("LineNumber: {0}{1}", ex.Errors[i].LineNumber, newLineCharacter);

                foreach(DictionaryEntry de in ex.Data) {
                    sb.AppendFormat("Data : {0} : {1}", de.Key, de.Value);
                }
                sb.Append(newLineCharacter);
            }
            return sb.ToString();
        }
    }
}
