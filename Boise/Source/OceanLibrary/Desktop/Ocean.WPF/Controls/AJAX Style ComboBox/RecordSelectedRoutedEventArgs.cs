
using System;
using System.Windows;

namespace Ocean.Wpf.Controls {
    
    /// <summary>
    /// Represents RecordSelectedRoutedEventArgs
    /// </summary>
    public class RecordSelectedRoutedEventArgs : RoutedEventArgs {

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public Object Value { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordSelectedRoutedEventArgs"/> class.
        /// </summary>
        public RecordSelectedRoutedEventArgs() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordSelectedRoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        public RecordSelectedRoutedEventArgs(RoutedEvent routedEvent)
            : base(routedEvent) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordSelectedRoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event identifier for this instance of the <see cref="T:System.Windows.RoutedEventArgs"/> class.</param>
        /// <param name="source">An alternate source that will be reported when the event is handled. This pre-populates the <see cref="P:System.Windows.RoutedEventArgs.Source"/> property.</param>
        public RecordSelectedRoutedEventArgs(RoutedEvent routedEvent, Object source)
            : base(routedEvent, source) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordSelectedRoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="source">The source.</param>
        /// <param name="value">The value.</param>
        public RecordSelectedRoutedEventArgs(RoutedEvent routedEvent, Object source, Object value)
            : base(routedEvent, source) {
            this.Value = value;
        }
    }
}