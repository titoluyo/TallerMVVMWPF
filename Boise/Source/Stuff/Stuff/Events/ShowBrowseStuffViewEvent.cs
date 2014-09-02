
using System;
using Microsoft.Practices.Composite.Presentation.Events;

namespace Stuff.Events {

    /// <summary>
    /// Prism Event Aggregator marker class.  Use to pass a type safe message from publisher to subscriber.
    /// </summary>
    public class ShowBrowseStuffViewEvent : CompositePresentationEvent<Object> { }
}
