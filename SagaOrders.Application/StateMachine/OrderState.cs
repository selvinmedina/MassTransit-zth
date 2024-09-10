using MassTransit;

namespace SagaOrders.Application.StateMachine
{
    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderId { get; set; }
        public required string CurrentState { get; set; }
        public decimal Amount { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? PaidAt { get; set; }
        public DateTime? CanceledAt { get; set; }
    }
}
