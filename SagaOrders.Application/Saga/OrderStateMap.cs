using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SagaOrders.Application.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaOrders.Application.Saga
{
    public class OrderStateMap : IEntityTypeConfiguration<OrderState>
    {
        public void Configure(EntityTypeBuilder<OrderState> builder)
        {
            builder.HasKey(x => x.CorrelationId);
            builder.Property(x => x.CorrelationId).ValueGeneratedNever();
            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.CurrentState).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired(false);
            builder.Property(x => x.PaidAt).IsRequired(false);
            builder.Property(x => x.CanceledAt).IsRequired(false);
        }
    }
}
