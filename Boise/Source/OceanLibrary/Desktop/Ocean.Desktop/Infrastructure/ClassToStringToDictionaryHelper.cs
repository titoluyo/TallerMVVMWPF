using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Ocean.Audit;
using Ocean.InputStringFormatting;
using Ocean.Properties;

namespace Ocean.Infrastructure {

    /// <summary>
    /// Represents ClassMessageCreationHelper, which provides helper methods to create audit log strings, complete class property values listings in either String or IDictionary formats.
    /// 
    /// This class was rewritten to work with Silverlight and use Silverlight friendly Reflection code.
    /// </summary>
    public class ClassMessageCreationHelper {

        #region Declarations

        const Int32 _ONE = 1;
        const String _DEFAULTVALUE = "DefaultValue";
        const String _NULL = "Null";
        private const String _FOURZEROS = "0000";

        #endregion

        #region  Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClassMessageCreationHelper"/> class.
        /// </summary>
        ClassMessageCreationHelper() { }

        #endregion

        #region  Methods

        /// <summary>
        /// Used to generate an Dictionary(Of String, String) for each property decorated with the AuditAttribute.
        /// </summary>
        /// <typeparam name="T">Class Type</typeparam>
        /// <param name="obj">Instance of class to audit</param>
        /// <param name="defaultValue">Default message if no class propeties are decorated</param>
        /// <returns>IDictionary</returns>
        public static IDictionary<String, String> AuditToIDictionary<T>(T obj, String defaultValue) {

            var dictionary = new Dictionary<String, String>();
            return AuditToIDictionary(obj, defaultValue, dictionary);
        }

        /// <summary>
        /// Used to generate an Dictionary(Of String, String) for each property decorated with the AuditAttribute.
        /// </summary>
        /// <typeparam name="T">Class Type</typeparam>
        /// <param name="obj">Instance of class to audit</param>
        /// <param name="defaultValue">Default message if no class propeties are decorated</param>
        /// <param name="dictionary">Pass an IDictionary Object that needs to be populated.  Typically this would be the Data property of an Exception Object.</param>
        /// <returns>IDictionary</returns>
        public static IDictionary<String, String> AuditToIDictionary<T>(T obj, String defaultValue, IDictionary<String, String> dictionary) {

            var list = new List<SortablePropertyBasket>();

            foreach(PropertyInfo prop in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {

                Object[] auditAttributes = prop.GetCustomAttributes(typeof(AuditAttribute), false);
                AuditAttribute auditAttribute = null;

                if(auditAttributes.Length == 1) {
                    auditAttribute = auditAttributes[0] as AuditAttribute;
                }

                if(auditAttribute != null) {
                    list.Add(new SortablePropertyBasket(auditAttribute.AuditSequence, prop.Name, CamelCaseString.GetWords(prop.Name), prop.GetValue(obj, null).ToString()));
                }

            }

            if(list.Count > 0) {
                list.Sort();

                foreach(SortablePropertyBasket propertyBasket in list) {
                    dictionary.Add(propertyBasket.PropertyName, propertyBasket.Value);
                }

            } else {
                dictionary.Add(_DEFAULTVALUE, defaultValue);
            }

            return dictionary;
        }

        /// <summary>
        /// This function returns a String of each property decorated with the AuditAttribute.  The String displays the property name, property friendly name and property value.  This function is typically used when a developer need to make an audit log entry.  It provides a very simple method to generate these messages.
        /// </summary>
        /// <typeparam name="T">Class Type</typeparam>
        /// <param name="obj">Instance of the class</param>
        /// <param name="defaultValue">If no properties have been decorated with the AuditAttribute, then this message is displayed.</param>
        /// <param name="delimiter">What delimiter do you want between each property.  Defaults to comma.  Could use vbcrlf, etc.</param>
        public static String AuditToString<T>(T obj, String defaultValue, String delimiter = GlobalConstants.STRING_DEFAULT_DELIMITER) {

            var sb = new StringBuilder(2048);
            var list = new List<SortablePropertyBasket>();

            foreach(PropertyInfo prop in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {

                Object[] auditAttributes = prop.GetCustomAttributes(typeof(AuditAttribute), false);
                AuditAttribute auditAttribute = null;

                if(auditAttributes.Length == 1) {
                    auditAttribute = auditAttributes[0] as AuditAttribute;
                }

                if(auditAttribute != null) {
                    list.Add(prop.GetValue(obj, null) != null ? new SortablePropertyBasket(auditAttribute.AuditSequence, prop.Name, CamelCaseString.GetWords(prop.Name), prop.GetValue(obj, null).ToString()) : new SortablePropertyBasket(auditAttribute.AuditSequence, prop.Name, CamelCaseString.GetWords(prop.Name), _NULL));
                }

            }

            if(list.Count > 0) {
                list.Sort();

                foreach(SortablePropertyBasket propertyBasket in list) {
                    sb.Append(propertyBasket.ToString());
                    sb.Append(delimiter);
                }

                if(sb.Length > delimiter.Length) {
                    sb.Length -= delimiter.Length;
                }

            } else {
                sb.Append(defaultValue);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Used to generate a Dictionary(Of String, String) for each property in the entity.  Dictionary is property name, property value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="sortByPropertyName">Name of the sort by property.</param>
        /// <returns></returns>
        public static IDictionary<String, String> ClassToIDictionary<T>(T obj, String defaultValue, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes) {

            var dictionary = new Dictionary<String, String>();
            return ClassToIDictionary(obj, defaultValue, dictionary, sortByPropertyName);
        }

        /// <summary>
        /// Used to generate a Dictionary(Of String, String) for each property in the entity.  Dictionary is property name, property value.  This method signature allows a IDictionary Object to be passed in.  This feature is useful when generating a name value pair dictionary to store in an Exception Object's Data property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The obj.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="sortByPropertyName">Name of the sort by property.</param>
        /// <returns></returns>
        public static IDictionary<String, String> ClassToIDictionary<T>(T obj, String defaultValue, IDictionary<String, String> dictionary, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes) {

            var list = new List<SortablePropertyBasket>();

            foreach(PropertyInfo prop in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {

                Object[] auditAttributes = prop.GetCustomAttributes(typeof(AuditAttribute), false);
                AuditAttribute auditAttribute = null;

                if(auditAttributes.Length == 1) {
                    auditAttribute = auditAttributes[0] as AuditAttribute;
                }

                if(auditAttribute != null) {

                    list.Add(new SortablePropertyBasket(_ONE, prop.Name, String.Empty, prop.GetValue(obj, null).ToString()));
                }
            }

            if(list.Count > 0) {

                if(sortByPropertyName == SortByPropertyName.Yes) {
                    list.Sort();
                }

                foreach(SortablePropertyBasket propertyBasket in list) {
                    dictionary.Add(propertyBasket.PropertyName, propertyBasket.Value);
                }

            } else {
                dictionary.Add(_DEFAULTVALUE, defaultValue);
            }

            return dictionary;
        }

        /// <summary>
        /// Returns a String with each property and value in the class.  The String displays the property name, property friendly name and property value.
        /// </summary>
        /// <typeparam name="T">Class Type</typeparam>
        /// <param name="obj">Instance of the class</param>
        /// <param name="delimiter">What delimiter do you want between each property.  Defaults to comma.  Could use vbcrlf, etc.</param>
        /// <param name="sortByPropertyName">Normally sorts the output by property name.  To leave in ordinal order, set to False</param>
        public static String ClassToString<T>(T obj, String delimiter = GlobalConstants.STRING_DEFAULT_DELIMITER, SortByPropertyName sortByPropertyName = SortByPropertyName.Yes) {

            var sb = new StringBuilder(4096);
            var list = new List<SortablePropertyBasket>();

            foreach(PropertyInfo prop in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {

                Object[] auditAttributes = prop.GetCustomAttributes(typeof(AuditAttribute), false);
                AuditAttribute auditAttribute = null;

                if(auditAttributes.Length == 1) {
                    auditAttribute = auditAttributes[0] as AuditAttribute;
                }

                Int32 auditSequence = 1;

                if(auditAttribute != null) {
                    auditSequence = auditAttribute.AuditSequence;
                }

                list.Add(prop.GetValue(obj, null) != null ? new SortablePropertyBasket(auditSequence, prop.Name, CamelCaseString.GetWords(prop.Name), prop.GetValue(obj, null).ToString()) : new SortablePropertyBasket(auditSequence, prop.Name, CamelCaseString.GetWords(prop.Name), _NULL));
            }

            if(list.Count > 0) {

                if(sortByPropertyName == SortByPropertyName.Yes) {
                    list.Sort();
                }

                foreach(SortablePropertyBasket propertyBasket in list) {
                    sb.Append(propertyBasket.ToString());
                    sb.Append(delimiter);
                }

                if(sb.Length > delimiter.Length) {
                    sb.Length -= delimiter.Length;
                }

            } else {
                sb.Append(Resources.ClassMessageCreationHelper_ClassToString_Class_has_no_properties);
            }

            return sb.ToString();
        }

        #endregion

        #region  Private Class(s)

        internal class SortablePropertyBasket : IComparable<SortablePropertyBasket> {

            #region  Properties

            /// <summary>
            /// Gets or sets the audit sequence.
            /// </summary>
            /// <value>The audit sequence.</value>
            public Int32 AuditSequence { get; private set; }

            /// <summary>
            /// Gets or sets the name of the friendly.
            /// </summary>
            /// <value>The name of the friendly.</value>
            public String FriendlyName { get; private set; }

            /// <summary>
            /// Gets or sets the name of the property.
            /// </summary>
            /// <value>The name of the property.</value>
            public String PropertyName { get; private set; }

            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>The value.</value>
            public String Value { get; private set; }

            #endregion

            #region  Constructors

            public SortablePropertyBasket(Int32 auditSequence, String propertyName, String friendlyName, String value) {

                if(value == null) {
                    value = String.Empty;
                }

                this.AuditSequence = auditSequence;
                this.PropertyName = propertyName;
                this.FriendlyName = friendlyName;
                this.Value = value;
            }

            #endregion

            #region  Methods

            public Int32 CompareTo(SortablePropertyBasket other) {
                return String.Compare(String.Concat(String.Concat(_FOURZEROS, this.AuditSequence.ToString()).Substring(String.Concat(_FOURZEROS, this.AuditSequence.ToString()).Length - 4), this.PropertyName), String.Concat(String.Concat("0000", other.AuditSequence.ToString()).Substring(String.Concat(_FOURZEROS, other.AuditSequence.ToString()).Length - 4), other.PropertyName));
            }

            public override String ToString() {
                return this.FriendlyName.Length == 0 ? String.Format("{0} = {1}", this.PropertyName, this.Value) : String.Format("{0} ( {1} ) = {2}", this.FriendlyName, this.PropertyName, this.Value);
            }

            #endregion
        }
        #endregion
    }
}
