
using Microsoft.Practices.Composite.Events;
using Simple.Core.Services.Container;
using Simple.Core.Services.Dialog;
using Simple.WPF.Services.Dialog;
using Stuff.Business;
using Stuff.Services.DataStore;

namespace Stuff.Services.Container {

    public static class ServiceLoader {
        public static void LoadDesignTimeServices() {
            ServiceContainer.Instance.AddService<IDialogService>(new ModalDialogService());
            ServiceContainer.Instance.AddService<IEventAggregator>(new EventAggregator());
            ServiceContainer.Instance.AddService<IRemoteDataStore>(new NetflixRemoteDataStore());
            ServiceContainer.Instance.AddService<IMovieDataStoreService>(MovieDataStoreService.Instance);
        }

        public static void LoadRunTimeServices() {
            ServiceContainer.Instance.AddService<IDialogService>(new ModalDialogService());
            ServiceContainer.Instance.AddService<IEventAggregator>(new EventAggregator());
            ServiceContainer.Instance.AddService<IRemoteDataStore>(new NetflixRemoteDataStore());
            ServiceContainer.Instance.AddService<IMovieDataStoreService>(MovieDataStoreService.Instance);
        }
    }
}
