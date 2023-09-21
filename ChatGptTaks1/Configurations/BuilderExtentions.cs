using Inventory.DAL.InventoryDbContext;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Configurations
{
    public static class BuilderExtentions
    {
        public static void ConfigureDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<InventorySystemContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}
