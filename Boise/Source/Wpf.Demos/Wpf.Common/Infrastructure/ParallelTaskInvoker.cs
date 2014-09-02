using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wpf.Common.Infrastructure {

    public class ParallelTaskInvoker {
        
        public void ExecuteParallelTask<T>(Func<T> repositoryMethod, Action<T> resultCallback, Action<Exception> errorCallback) where T : class {
            if (repositoryMethod == null) throw new ArgumentNullException("repositoryMethod");
            if (resultCallback == null) throw new ArgumentNullException("resultCallback");
            if (errorCallback == null) throw new ArgumentNullException("errorCallback");

            var task = Task.Factory.StartNew(() => {
                try {
                    return new RepositoryResult<T>(repositoryMethod(), null);
                } catch (Exception ex) {
                    return new RepositoryResult<T>(null, ex);
                }
            });

            task.ContinueWith(r => {
                if (r.Result.Error != null) {
                    errorCallback(r.Result.Error);
                } else {
                    resultCallback(r.Result.Package);
                }
            }, CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}
