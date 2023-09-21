namespace Inventory.DAL.UoW.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        T GetRepository<T>();
        Task SaveAsync();
    }
}
