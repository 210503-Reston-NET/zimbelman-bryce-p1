using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;
using StoreBL;
using StoreDL;

namespace StoreUI
{
    /// <summary>
    /// Class to manufacture menus using factory design pattern
    /// </summary>
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuType) {
            // Getting configurations from a config file
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            
            // Setting up DB Connections
            string connectionString = configuration.GetConnectionString("MochaMoment");
            DbContextOptions<MochaMomentDBContext> options = new DbContextOptionsBuilder<MochaMomentDBContext>()
            .UseSqlServer(connectionString).Options;

            var context = new MochaMomentDBContext(options);

            switch (menuType.ToLower()) {
                case "main":
                    return new MainMenu();

                    case "customer":
                        return new CustomerMenu(new CustomerBL(new RepoDB(context)), new ProductBL(new RepoDB(context)), new OrderBL(new RepoDB(context)), new LocationBL(new RepoDB(context)), new LineItemBL(new RepoDB(context)), new InventoryBL(new RepoDB(context), new LocationBL(new RepoDB(context))), new ValidationService());

                    case "manager":
                        return new ManagerMenu(new CustomerBL(new RepoDB(context)), new LocationBL(new RepoDB(context)), new ProductBL(new RepoDB(context)), new InventoryBL(new RepoDB(context), new LocationBL(new RepoDB(context))), new OrderBL(new RepoDB(context)), new LineItemBL(new RepoDB(context)), new ValidationService());

                default:
                    return null;
            }
        }
    }
}