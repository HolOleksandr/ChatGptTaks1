using Inventory.BLL.Exceptions;
using Inventory.BLL.Services.Implementations;
using Inventory.DAL.Repositories.Interfaces;
using Inventory.DAL.UoW.Interfaces;
using Inventory.Models;
using Moq;

namespace Inventory.UnitTests.Services
{
    public class ProductServiceTests
    {
        private ProductService _productService;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IProductRepository> _productRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock.Setup(uow => uow.GetRepository<IProductRepository>()).Returns(_productRepositoryMock.Object);

            _productService = new ProductService(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task GetProductById_ProductExists_ReturnsProduct()
        {
            // Arrange
            int productId = 1;
            var expectedProduct = new Product { Id = productId, Name = "Sample Product", Price = 10.0M };
            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(expectedProduct);

            // Act
            var result = await _productService.GetProductById(productId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedProduct));
        }

        [Test]
        public void GetProductById_ProductDoesNotExist_ThrowsProductNotFoundException()
        {
            // Arrange
            int productId = 1;
            _ = _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).Returns(Task.FromResult<Product?>(null));

            // Act & Assert
            Assert.ThrowsAsync<ProductNotFoundException>(() => _productService.GetProductById(productId));
        }
        [Test]
        public async Task GetAllProducts_ReturnsProductList()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10 },
                new Product { Id = 2, Name = "Product 2", Price = 20 },
            };
            _productRepositoryMock.Setup(repo => repo.GetAllProducts()).ReturnsAsync(expectedProducts);

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(expectedProducts));
        }

        [Test]
        public async Task AddProduct_ProductIsValid_ReturnsAddedProduct()
        {
            // Arrange
            var newProduct = new Product { Name = "Product 1", Price = 10 };
            var addedProduct = new Product { Id = 1, Name = "Product 1", Price = 10 };
            _productRepositoryMock.Setup(repo => repo.AddProduct(newProduct)).ReturnsAsync(addedProduct);

            // Act
            var result = await _productService.AddProduct(newProduct);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(addedProduct));
        }

        [Test]
        public async Task UpdateProduct_ProductExists_ReturnsUpdatedProduct()
        {
            // Arrange
            int productId = 1;
            var product = new Product { Id = productId, Name = "Product 1", Price = 10 };
            var updatedProduct = new Product { Id = productId, Name = "Updated Product 1", Price = 15 };
            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(product);
            _productRepositoryMock.Setup(repo => repo.UpdateProduct(updatedProduct)).ReturnsAsync(updatedProduct);

            // Act
            var result = await _productService.UpdateProduct(updatedProduct);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EqualTo(updatedProduct));
        }

        [Test]
        public void UpdateProduct_ProductDoesNotExist_ThrowsProductNotFoundException()
        {
            // Arrange
            int productId = 1;
            var updatedProduct = new Product { Id = productId, Name = "Updated Product 1", Price = 15 };
            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).Returns(Task.FromResult<Product?>(null));

            // Act & Assert
            Assert.ThrowsAsync<ProductNotFoundException>(() => _productService.UpdateProduct(updatedProduct));
        }

        [Test]
        public async Task DeleteProduct_ProductExists_RemovesProduct()
        {
            // Arrange
            int productId = 1;
            var product = new Product { Id = productId, Name = "Product 1", Price = 10 };
            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).ReturnsAsync(product);
            _productRepositoryMock.Setup(repo => repo.DeleteProduct(product)).Returns(Task.CompletedTask);

            // Act
            await _productService.DeleteProduct(productId);

            // Assert
            _productRepositoryMock.Verify(repo => repo.DeleteProduct(product), Times.Once);
        }

        [Test]
        public void DeleteProduct_ProductDoesNotExist_ThrowsProductNotFoundException()
        {
            // Arrange
            int productId = 1;
            _productRepositoryMock.Setup(repo => repo.GetProductById(productId)).Returns(Task.FromResult<Product>(null));

            // Act & Assert
            Assert.ThrowsAsync<ProductNotFoundException>(() => _productService.DeleteProduct(productId));
        }
    }
}
