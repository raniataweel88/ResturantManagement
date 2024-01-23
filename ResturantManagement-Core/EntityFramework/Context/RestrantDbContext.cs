using Microsoft.EntityFrameworkCore;
using ResturantManagement_Core.EntityFramework.Models;
using ResturantManagement_Core.EntityFramework.Models.EntityConfigration;
using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Context
{
    public class RestrantDbContext : DbContext
    {
        public RestrantDbContext(DbContextOptions<RestrantDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerEntityConfigration());
            modelBuilder.ApplyConfiguration(new EmployeEntityConfigration());
            modelBuilder.ApplyConfiguration(new MenuEntityConfigration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfigration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfigration());
            modelBuilder.ApplyConfiguration(new TableEntityConfigration());
        }
        public virtual DbSet<Employe>Employes { get; set; }
        public  virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }  
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Table> Tables { get; set; }

    }
}
