using HelloApi.Consumers;
using HelloApi.Cotracts;
using HelloApi.Filters;
using MassTransit;
using MassTransit.Transports.Fabric;
using System.Reflection;
namespace HelloApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddOptions<RabbitMqTransportOptions>().BindConfiguration("RabbitMq");
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Tenant>();

            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<MessageConsumer>();
                
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseSendFilter(typeof(TenantSendFilter<>), context);
                    //cfg.UsePublishFilter(typeof(TenantPublishFilter<>), context);
                    cfg.UsePublishFilter(typeof(TenantPublishFilter<>), context);
                    cfg.UseConsumeFilter(typeof(TenantConsumeFilter<>), context,
                        x=> x.Include(typeof(Message)));
                    //cfg.UsePublishFilter<TenantPublishMessageFilter>(context);

                    cfg.ConfigurePublish(x =>
                    {
                        x.UseFilter<Email>(new TenantPublishMessageFilter());
                    });

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
