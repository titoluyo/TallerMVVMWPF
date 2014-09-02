
using System;
using System.Collections.Generic;

namespace Simple.Core.Services.Container {

    /// <summary>
    /// Provides a singleton container for storing and retrieving services by <see cref="Type"/>.
    /// </summary>
    public class ServiceContainer {

        #region Declarations

        readonly Object _lockObject = new Object();
        readonly Dictionary<Type, object> _services = new Dictionary<Type, Object>();

        /// <summary>
        /// Provides access to the ServiceLocator singleton.
        /// </summary>
        public static readonly ServiceContainer Instance = new ServiceContainer();

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this <c>ServiceLocator</c> has any stored services.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public Boolean IsEmpty {
            get {
                lock (_lockObject) {
                    return _services.Count == 0;
                }
            }
        }

        #endregion

        #region Private Constructor

        ServiceContainer() { }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a service implementation to the repository.  If the service already exists, it will replace it.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of service to retrieve.</typeparam>
        /// <param name="service">Instance of the service to add.</param>
        public void AddService<T>(T service)
            where T : class {
            lock (_lockObject) {
                _services[typeof(T)] = service;
            }
        }

        /// <summary>
        /// Gets an instance of the requested service if available.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of service to retrieve.</typeparam>
        /// <returns>The requested instance of the service or null.</returns>
        public T GetService<T>()
            where T : class {
            object service;
            lock (_lockObject) {
                _services.TryGetValue(typeof(T), out service);
            }
            return service as T;
        }

        #endregion
    }
}
