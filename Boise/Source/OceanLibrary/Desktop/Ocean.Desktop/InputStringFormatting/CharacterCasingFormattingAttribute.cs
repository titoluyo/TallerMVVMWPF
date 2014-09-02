using System;

namespace Ocean.InputStringFormatting {

    /// <summary>
    /// Represents CharacterCasingFormattingAttribute, when applied to a business entity class property, that property will have its case corrected according to the CharacterCasing assigned.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class CharacterCasingFormattingAttribute : Attribute {

        #region  Properties

        /// <summary>
        /// Get or sets the CharacterCasing that will be applied to this property when the property is updated.
        /// </summary>
        public CharacterCasing CharacterCasing { get; private set; }

        #endregion

        #region  Constructors and Load & Unload

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterCasingFormattingAttribute"/> class.
        /// </summary>
        /// <param name="characterCasing">The character casing.</param>
        public CharacterCasingFormattingAttribute(CharacterCasing characterCasing) {
            this.CharacterCasing = characterCasing;
        }

        #endregion
    }
}