using System;

namespace Ocean.ExtensionMethods {

    /// <summary>
    /// Represents DateTimeExtensions
    /// </summary>
    public static class DateTimeExtensions {

        /// <summary>
        /// Initializes the <see cref="DateTimeExtensions"/> class.
        /// </summary>
        static DateTimeExtensions() { }

        /// <summary>
        ///  Retuns a DateTeime with the Time set to date minimum time.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        public static DateTime ToDateMinTime(this DateTime dateTime) {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
