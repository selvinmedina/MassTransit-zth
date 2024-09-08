using HelloApi.Consumers;
using HelloApi.Cotracts;
using MassTransit;
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

            builder.Services.AddMassTransit(x =>
            {
                //var entryAssemly = Assembly.GetEntryAssembly();
                //x.AddConsumers(entryAssemly);
                //x.SetKebabCaseEndpointNameFormatter();
                //x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("hellos", true));
                //x.AddConsumers(typeof(MessageConsumer));

                //x.AddConsumer<MessageConsumer>();
                //x.AddConsumer<MessageConsumer, MessageConsumerDefinition>();
                //x.AddConsumer<MessageConsumer>()
                //.Endpoint(e => e.Name = "salutation");

                x.UsingRabbitMq((context, cfg) =>
                {
                    //cfg.Host("localhost", "/", h =>
                    //{
                    //    h.Username("guest");
                    //    h.Password("guest");
                    //});
                    //cfg.ReceiveEndpoint("manually-configured", e =>
                    //{
                    //    e.UseMessageRetry(r => r.Interval(5, TimeSpan.FromSeconds(1)));
                    //    e.ConfigureConsumer<MessageConsumer>(context);
                    //    e.ConfigureConsumeTopology = false;
                    //});
                    //cfg.Message<Message>(x => x.SetEntityName("my-message"));
                    //cfg.ConfigureEndpoints(context);
                });

                //x.UsingInMemory(); // for testing
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
