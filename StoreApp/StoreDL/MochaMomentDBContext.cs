using Microsoft.EntityFrameworkCore;
using StoreModels;

namespace StoreDL
{
    public class MochaMomentDBContext : DbContext
    {
        public MochaMomentDBContext(DbContextOptions options) : base(options) {

        }

        protected MochaMomentDBContext() {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories {get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Order> Orders {get; set; }
        public DbSet<Product> Products {get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Customer>().Property(customer => customer.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Inventory>().Property(inventory => inventory.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<LineItem>().Property(lineItem => lineItem.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Location>().Property(location => location.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Order>().Property(order => order.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Product>().Property(product => product.Id).ValueGeneratedOnAdd();
        }
    }
}