using Inventory.DAL.InventoryDbContext;
using Inventory.DAL.Repositories.Interfaces;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.DAL.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventorySystemContext _context;

        public ProductRepository(InventorySystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product?>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            await Task.Run(() => _context.Products.Update(product));
            return product;
        }

        public async Task DeleteProduct(Product product)
        {
            await Task.Run(() => _context.Products.Remove(product));
        }
    }
}
