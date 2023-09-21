using Inventory.DAL.InventoryDbContext;
using Inventory.DAL.Repositories.Implementations;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.UnitTests.Repositories
{
    public class ProductRepositoryTests
    {
        private InventorySystemContext _context;
        private ProductRepository _productRepository;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<InventorySystemContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new InventorySystemContext(options);
            _productRepository = new ProductRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        private async Task SeedData()
        {
            _context.Products.AddRange(
                new Product { Id = 1, Name = "Product 1", Price = 10 ,Description = "Description 1"},
                new Product { Id = 2, Name = "Product 2", Price = 20 ,Description = "Description 2"}
            );
            await _context.SaveChangesAsync();
        }

        [Test]
        public async Task GetAllProducts_Returns_ProductList()
        {
            // Arrange
            await SeedData();

            // Act
            var result = await _productRepository.GetAllProducts();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetProductById_ProductExists_Returns_Product()
        {
            // Arrange
            await SeedData();
            int productId = 1;

            // Act
            var result = await _productRepository.GetProductById(productId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(productId));
        }

        [Test]
        public async Task GetProductById_ProductDoesNotExist_Returns_Null()
        {
            // Arrange
            await SeedData();
            int productId = 3;

            // Act
            var result = await _productRepository.GetProductById(productId);

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public async Task AddProduct_ProductIsValid_Returns_AddedProduct()
        {
            // Arrange
            await SeedData();
            var newProduct = new Product { Name = "Product 3", Price = 30 , Description = "Description 1"};

            // Act
            var result = await _productRepository.AddProduct(newProduct);
            await _context.SaveChangesAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo(3));
                Assert.That(_context.Products.Count(), Is.EqualTo(3));
            });
        }

        [Test]
        public async Task UpdateProduct_ProductExists_Returns_UpdatedProduct()
        {
            // Arrange
            await SeedData();
            var originalProduct = await _context.Products.FindAsync(1);
            if (originalProduct != null)
            {
                _ = _context.Entry(originalProduct).State = EntityState.Detached;
            }

            var updatedProduct = new Product { Id = 1, Name = "Updated Product 1", Price = 15 };

            // Act
            var result = await _productRepository.UpdateProduct(updatedProduct);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(updatedProduct.Name));
                Assert.That(result.Price, Is.EqualTo(updatedProduct.Price));
            });
        }

        [Test]
        public async Task DeleteProduct_ProductExists_RemovesProduct()
        {
            // Arrange
            await SeedData();
            var productToDelete = _context.Products.First(p => p.Id == 2);

            // Act
            await _productRepository.DeleteProduct(productToDelete);
            await _context.SaveChangesAsync();
            var deletedProduct = await _productRepository.GetProductById(2);
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deletedProduct, Is.Null);
                Assert.That(_context.Products.Count(), Is.EqualTo(1));
            });
        }
    }
}