
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SagaOrders.Application.Data;
using SagaOrders.Application.Services;
using SagaOrders.Application.StateMachine;
using SagaOrders.Infrastructure;

namespace Orders
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHostedService<RecreateDatabaseHostedService<OrdersDbContext>>();

            builder.Services.AddDbContext<OrdersDbContext>(x =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MyStoreDb");

                x.UseNpgsql(connectionString, options =>
                {

                    //options.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    options.MigrationsHistoryTable($"__{nameof(OrdersDbContext)}");

                    options.EnableRetryOnFailure(5);
                    options.MinBatchSize(1);

                });

            });

            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddMassTransit(x =>
            {
                //x.AddSagaStateMachine<OrderStateMachine, OrderState>()
                //    .InMemoryRepository();

                x.AddSagaStateMachine<OrderStateMachine, OrderState>()
                    .EntityFrameworkRepository(r =>
                    {
                        r.ConcurrencyMode = ConcurrencyMode.Optimistic;
                        r.ExistingDbContext<OrdersDbContext>();
                    });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
