
using System;

namespace Ocean.Audit {

    /// <summary>
    /// Represents AuditAttribute, when applied to a business entity class property, that property will participate in audit message creation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class AuditAttribute : Attribute {

        #region  Declarations

        readonly Int32 _auditSequence = 999999;

        #endregion

        #region  Properties

        /// <summary>
        /// Get the sort order that will be applied to this property when audit messages are created.  Default value is 999999.
        /// </summary>
        public Int32 AuditSequence {
            get {
                return _auditSequence;
            }
        }

        #endregion

        #region  Constructor

        /// <summary>
        /// AuditSequence, sort order will default to 999999 when using this default constructor.
        /// </summary>
        public AuditAttribute() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditAttribute"/> class.
        /// </summary>
        /// <param name="auditSequence">The audit sequence sort order.</param>
        public AuditAttribute(Int32 auditSequence) {
            _auditSequence = auditSequence;
        }

        #endregion
    }
}

