using Inventory.DAL.InventoryDbContext;
using Inventory.DAL.Repositories.Implementations;
using Inventory.DAL.Repositories.Interfaces;
using Inventory.DAL.UoW.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.UnitTests.UoW
{
    public class UnitOfWorkTests
    {
        private UnitOfWork _unitOfWork;
        private IServiceProvider _serviceProvider;

        [SetUp]
        public void SetUp()
        {
            var services = new ServiceCollection();
            services.AddDbContext<InventorySystemContext>(options =>
                options.UseInMemoryDatabase(databaseName: "TestDb_UoW"));

            services.AddScoped<IProductRepository, ProductRepository>();

            _serviceProvider = services.BuildServiceProvider();
            _unitOfWork = new UnitOfWork(_serviceProvider.GetRequiredService<InventorySystemContext>(), _serviceProvider);
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Dispose();
        }

        [Test]
        public void GetRepository_Returns_Correct_Repository_Type()
        {
            // Act
            var productRepository = _unitOfWork.GetRepository<IProductRepository>();

            // Assert
            Assert.That(productRepository, Is.Not.Null);
            Assert.That(productRepository, Is.InstanceOf<ProductRepository>());
        }

        [Test]
        public void GetRepository_Throws_Exception_For_NonExisting_Type()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => _unitOfWork.GetRepository<INonExistingRepository>());
        }
    }
}
