using Microsoft.Practices.Prism.Events;

namespace Wpf.Common.Events {

    public interface IEventResolver<out T> where T : EventBase, new() {
        T Resolve();
    }
}
