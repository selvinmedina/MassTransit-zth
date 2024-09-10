using MassTransit;
using SagaOrders.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaOrders.Application.StateMachine
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public State Pending { get; private set; }
        public State Paid { get; private set; }
        public State Canceled { get; private set; }

        public Event<OrderCreated> OrderCreated { get; private set; }
        public Event<PayOrder> PayOrder { get; private set; }
        public Event<CancelOrder> CancelOrder { get; private set; }

        public OrderStateMachine()
        {
            InstanceState(x => x.CurrentState);
            Event(() => OrderCreated, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => PayOrder, x => x.CorrelateById(m => m.Message.OrderId));
            Event(() => CancelOrder, x => x.CorrelateById(m => m.Message.OrderId));

            Initially(
                When(OrderCreated)
                    .Then(context =>
                    {
                        Console.WriteLine("Order created: {0}", context.Data.OrderId);
                        context.Instance.OrderId = context.Data.OrderId;
                        context.Instance.Amount = context.Data.Amount;
                        context.Instance.CreatedAt = context.Data.CreatedAt;
                        })
                    .TransitionTo(Pending)
            );

            During(Pending,
                When(PayOrder)
                    .Then(context =>
                    {
                        Console.WriteLine("Order paid: {0}", context.Data.OrderId);
                        context.Saga.Amount = context.Message.Amount;
                        context.Saga.PaidAt = DateTime.UtcNow;
                    })
                    .TransitionTo(Paid)
                    .Publish(context => new OrderPaid { OrderId = context.Saga.OrderId}),
                When(CancelOrder)
                    .Then(context =>
                    {
                        Console.WriteLine("Order canceled: {0}", context.Data.OrderId);
                        context.Saga.CanceledAt = DateTime.UtcNow;
                    })
                    .TransitionTo(Canceled)
                    .Publish(context => new OrderCancelled { OrderId = context.Saga.OrderId})
            );
        }
    }
}
