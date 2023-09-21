using Inventory.BLL.Exceptions;
using Inventory.BLL.Services.Interfaces;
using Inventory.DAL.Repositories.Interfaces;
using Inventory.DAL.UoW.Interfaces;
using Inventory.Models;

namespace Inventory.BLL.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product?>> GetAllProducts()
        {
            var products = await _unitOfWork.GetRepository<IProductRepository>().GetAllProducts();

            return products ?? Enumerable.Empty<Product>();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _unitOfWork.GetRepository<IProductRepository>().GetProductById(id);
            return product ?? throw new ProductNotFoundException($"Product with ID: {id} not found.");
        }

        public async Task<Product> AddProduct(Product product)
        {
            var newProduct = await _unitOfWork.GetRepository<IProductRepository>().AddProduct(product);
            await _unitOfWork.SaveAsync();
            return newProduct;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _ = await GetProductById(product.Id);
            var updatedProduct = await _unitOfWork.GetRepository<IProductRepository>().UpdateProduct(product);
            await _unitOfWork.SaveAsync();
            return updatedProduct;
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetProductById(id);
            await _unitOfWork.GetRepository<IProductRepository>().DeleteProduct(product);
            await _unitOfWork.SaveAsync();
        }
    }
}
