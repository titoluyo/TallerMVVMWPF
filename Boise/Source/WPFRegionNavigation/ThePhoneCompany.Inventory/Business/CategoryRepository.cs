using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThePhoneCompany.Common.Infrastructure;
using ThePhoneCompany.Inventory.Service;
using ThePhoneCompany.Inventory.ThePhoneCompanyDataService;

namespace ThePhoneCompany.Inventory.Business {

    [Export(typeof(ICategoryRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CategoryRepository : ICategoryRepository {

        readonly ThePhoneCompanyEntities _dataService;

        [ImportingConstructor]
        public CategoryRepository(DataServiceFacade dataServiceFacade) {
            _dataService = dataServiceFacade.DataService;
        }

        void ICategoryRepository.GetAll(Action<IEnumerable<Category>> resultCallback, Action<Exception> errorCallback) {

            // This code can be refactored into a generic method
            // I left it this way to help with the learning process 

            Task<RepositoryResult<IEnumerable<Category>>> task =
                Task.Factory.StartNew(() => {
                    try {
                        return new RepositoryResult<IEnumerable<Category>>(_dataService.Categories.ToList(), null);
                    } catch(Exception ex) {
                        return new RepositoryResult<IEnumerable<Category>>(null, ex);
                    }
                });

            task.ContinueWith(r => {
                if(r.Result.Error != null) {
                    errorCallback(r.Result.Error);
                } else {
                    resultCallback(r.Result.Package);
                }
            }, CancellationToken.None, TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());

        }

        void ICategoryRepository.Get(Int32 categoryId, Action<Category> resultCallback, Action<Exception> errorCallback) {
            
            if(categoryId != 0) {

                // This code can be refactored into a generic method
                // I left it this way to help with the learning process 

                Task<RepositoryResult<Category>> task =
                    Task.Factory.StartNew(() => {
                        try {
                            return new RepositoryResult<Category>(_dataService.Categories.Where(c => c.CategoryID == categoryId).FirstOrDefault(), null);
                        } catch(Exception ex) {
                            return new RepositoryResult<Category>(null, ex);
                        }
                    });

                task.ContinueWith(r => {
                    if(r.Result.Error != null) {
                        errorCallback(r.Result.Error);
                    } else {
                        resultCallback(r.Result.Package);
                    }
                }, CancellationToken.None, TaskContinuationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext());
            } else {
                resultCallback(((ICategoryRepository)this).Create());
            }
        }

        // I always create my entity objects in the business layer and never in the presentation layer
        Category ICategoryRepository.Create() {
            return new Category();
        }
    }
}
