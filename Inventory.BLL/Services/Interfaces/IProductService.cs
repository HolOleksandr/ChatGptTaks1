using Inventory.Models;

namespace Inventory.BLL.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product?>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
