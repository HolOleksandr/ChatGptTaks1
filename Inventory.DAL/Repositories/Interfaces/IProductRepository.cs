using Inventory.Models;

namespace Inventory.DAL.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product?>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}
