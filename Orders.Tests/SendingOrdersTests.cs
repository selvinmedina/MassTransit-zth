using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using SagaOrders.Application.Events;

namespace Orders.Tests
{
    [TestFixture]
    public class SendingOrdersTests
    {
        [Test]
        public async Task Should_Publish_OrderCreated_Event()
        {
            // Arrange
            // var harness = new InMemoryTestHarness();
            // var harnessRabbit = new RabbitMqTestHarness();

            await using var provider = new ServiceCollection()
       .ConfigureRabbitMqTestOptions(r =>
       {
           r.CreateVirtualHostIfNotExists = true;
           //  r.CleanVirtualHost = true;
       })
       .AddMassTransitTestHarness(x =>
       {
           x.AddConsumer<TestingHarnessSubmitOrderConsumer>();

           x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));

       })
       .BuildServiceProvider(true);

            var harness = provider.GetTestHarness();
            ;

            var order = new SagaOrders.Models.OrderModel()
            {
                Comments = "Test",
                Total = 10
            };

            //harness.Consumer<OrderCreated>();

            await harness.Start();
            try
            {
                // Act
                for (int i = 0; i < 100; i++)
                {
                    await harness.Bus.Publish(new OrderCreated()
                    {
                        Amount = order.Total,
                        CreatedAt = DateTime.Now,
                        OrderId = Guid.NewGuid()
                    });

                }

                //await Assert.MultipleAsync(async () =>
                //{
                //    Assert.That(await harness.Sent.Any<OrderCreated>(), Is.True);

                //   // Assert.That(await harness.Consumed.Any<OrderCreated>(), Is.True);
                //});
                // Assert
                // Assert.That(await harness.Published.Any<OrderCreated>(), Is.True);
                // Assert.That(harness.Published.Select<OrderCreated>().Count(), Is.EqualTo(1));

                var publishedEvent = harness.Published.Select<OrderCreated>().First();
                Assert.That(order.Total, Is.EqualTo(publishedEvent.Context.Message.Amount));
            }
            finally
            {
                await harness.Stop();
            }
        }
    }
}