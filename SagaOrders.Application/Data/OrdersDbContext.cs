using Microsoft.EntityFrameworkCore;
using SagaOrders.Application.Data.Entities;


namespace SagaOrders.Application.Data
{
    public class OrdersDbContext : DbContext
    {


        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
       : base(options)
        {


        }

        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
