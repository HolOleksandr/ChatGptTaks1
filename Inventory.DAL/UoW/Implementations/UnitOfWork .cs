using Inventory.DAL.InventoryDbContext;
using Inventory.DAL.UoW.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.DAL.UoW.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventorySystemContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<Type, object> _repositories;

        public UnitOfWork(InventorySystemContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
            _serviceProvider = serviceProvider;
        }

        public T GetRepository<T>()
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                T repositoryInstance = _serviceProvider.GetService<T>()
                    ?? throw new ArgumentNullException($"Repository {type.Name} doesn't exist");
                _repositories.Add(type, repositoryInstance);
            }
            return (T)_repositories[type];
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
