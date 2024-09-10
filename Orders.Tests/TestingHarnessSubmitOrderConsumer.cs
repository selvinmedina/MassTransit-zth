using MassTransit;
using MassTransit.TestFramework.ForkJoint.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Tests
{
    class TestingHarnessSubmitOrderConsumer :
              IConsumer<SubmitOrder>
    {
        public Task Consume(ConsumeContext<SubmitOrder> context)
        {

            TestContext.WriteLine($"Order {context.Message.OrderId} got it!");
            return Task.CompletedTask;
        }
    }
}
