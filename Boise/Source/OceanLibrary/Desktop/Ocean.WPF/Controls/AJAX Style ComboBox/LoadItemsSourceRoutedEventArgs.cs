
using System;
using System.Windows;

namespace Ocean.Wpf.Controls {

    /// <summary>
    /// Represents LoadItemsSourceRoutedEventArgs
    /// </summary>
    public class LoadItemsSourceRoutedEventArgs : RoutedEventArgs {

        String _searchString = String.Empty;

        /// <summary>
        /// Gets or sets the search string.
        /// </summary>
        /// <value>The search string.</value>
        public String SearchString {
            get {
                return _searchString;
            }
            set {
                _searchString = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadItemsSourceRoutedEventArgs"/> class.
        /// </summary>
        public LoadItemsSourceRoutedEventArgs() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadItemsSourceRoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        public LoadItemsSourceRoutedEventArgs(RoutedEvent routedEvent)
            : base(routedEvent) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadItemsSourceRoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event identifier for this instance of the <see cref="T:System.Windows.RoutedEventArgs"/> class.</param>
        /// <param name="source">An alternate source that will be reported when the event is handled. This pre-populates the <see cref="P:System.Windows.RoutedEventArgs.Source"/> property.</param>
        public LoadItemsSourceRoutedEventArgs(RoutedEvent routedEvent, Object source)
            : base(routedEvent, source) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadItemsSourceRoutedEventArgs"/> class.
        /// </summary>
        /// <param name="routedEvent">The routed event.</param>
        /// <param name="source">The source.</param>
        /// <param name="searchString">The search string.</param>
        public LoadItemsSourceRoutedEventArgs(RoutedEvent routedEvent, Object source, String searchString)
            : base(routedEvent, source) {
            _searchString = searchString;
        }
    }
}

