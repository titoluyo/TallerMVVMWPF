using System;
using System.Data.Entity;

namespace UnityTutorials.Models
{
    public interface IUnitOfWorkManager : IDisposable
    {
        IUnitOfWork NewUnitOfWork();
    }
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private bool _isDisposed;
        private readonly WebDbContext _context;

        public UnitOfWorkManager(IWebDbContext context)
        {
            Database.SetInitializer<WebDbContext>(null);
            this._context = context as WebDbContext;
        }

        public IUnitOfWork NewUnitOfWork()
        {
            return new UnitOfWork(this._context);
        }

        public void Dispose()
        {
            if (this._isDisposed == false)
            {
                this._context.Dispose();
                this._isDisposed = true;
            }
        }
    }
}