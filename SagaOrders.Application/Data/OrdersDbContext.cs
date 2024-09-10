using Microsoft.EntityFrameworkCore;
using SagaOrders.Application.Data.Entities;
using SagaOrders.Application.Saga;
using SagaOrders.Application.StateMachine;


namespace SagaOrders.Application.Data
{
    public class OrdersDbContext : DbContext
    {


        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
       : base(options)
        {


        }

        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderState> OrderState { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderStateMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
