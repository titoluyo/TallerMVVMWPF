using System;
using System.Collections.Generic;

namespace Ocean.Container {
    /// <summary>
    /// Represents SimpleServiceContainer that provides a singleton container for storing and retrieving services by <see cref="Type"/>.
    /// Good for small and simple projects.  
    /// For projects larger than a small project, prefer Unity for IOC.
    /// </summary>
    /// <example>
    /// Example shows how to load the container at design-time and run-time
    /// 
    /// public static class ServiceLoader {
    ///     public static void LoadDesignTimeServices() {
    ///         ServiceContainer.Instance.AddService&gt;IDialogService &lt;(new ModalDialogService());
    ///     }
    /// 
    ///     public static void LoadRunTimeServices() {
    ///         ServiceContainer.Instance.AddService&gt;IDialogService &lt;(new ModalDialogService());
    ///     }
    /// }
    /// </example>
    public class SimpleServiceContainer {

        #region Declarations

        readonly Object _lockObject = new Object();
        readonly Dictionary<Type, Object> _services = new Dictionary<Type, Object>();
        static SimpleServiceContainer _instance;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static SimpleServiceContainer Instance {
            get { return _instance ?? (_instance = new SimpleServiceContainer()); }
        } 
        
        /// <summary>
        /// Gets a value indicating whether this <c>ServiceLocator</c> has any stored services.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        public Boolean IsEmpty {
            get {
                lock(_lockObject) {
                    return _services.Count == 0;
                }
            }
        }

        #endregion

        #region Private Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleServiceContainer"/> class.
        /// </summary>
        SimpleServiceContainer() { }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a service implementation to the repository.  If the service already exists, it will replace it.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of service to retrieve.</typeparam>
        /// <param name="service">Instance of the service to add.</param>
        public void AddService<T>(T service)
            where T : class {
            lock(_lockObject) {
                _services[typeof(T)] = service;
            }
        }

        /// <summary>
        /// Gets an instance of the requested service if available.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of service to retrieve.</typeparam>
        /// <returns>The requested instance of the service or null.</returns>
        public T GetService<T>() where T : class {
            Object service;
            lock(_lockObject) {
                _services.TryGetValue(typeof(T), out service);
            }
            return service as T;
        }

        #endregion
    }
}
