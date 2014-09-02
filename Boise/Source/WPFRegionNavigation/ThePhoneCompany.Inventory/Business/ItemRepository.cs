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
    
    [Export(typeof(IItemRepository))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ItemRepository : IItemRepository {

        readonly ThePhoneCompanyEntities _dataService;
        
        [ImportingConstructor]
        public ItemRepository(DataServiceFacade dataServiceFacade) {
            _dataService = dataServiceFacade.DataService;
        }

        void IItemRepository.GetAll(Action<IEnumerable<Item>> resultCallback, Action<Exception> errorCallback) {

            // This code can be refactored into a generic method
            // I left it this way to help with the learning process 
            Task<RepositoryResult<IEnumerable<Item>>> task =
                Task.Factory.StartNew(() => {
                    try {
                        return new RepositoryResult<IEnumerable<Item>>(_dataService.Items.ToList(), null);
                    } catch(Exception ex) {
                        return new RepositoryResult<IEnumerable<Item>>(null, ex);
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

        void IItemRepository.Get(Int32 itemId, Action<Item> resultCallback, Action<Exception> errorCallback) {

            if(itemId != 0) {

                // This code can be refactored into a generic method
                // I left it this way to help with the learning process 
                Task<RepositoryResult<Item>> task =
                    Task.Factory.StartNew(() => {
                        try {
                            return new RepositoryResult<Item>(_dataService.Items.Where(i => i.ItemID == itemId).FirstOrDefault(), null);
                        } catch(Exception ex) {
                            return new RepositoryResult<Item>(null, ex);
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
                resultCallback(((IItemRepository)this).Create());
            }
        }

        // I always create my entity objects in the business layer and never in the presentation layer
        Item IItemRepository.Create() {
            return new Item(); 
        }
    }
}
