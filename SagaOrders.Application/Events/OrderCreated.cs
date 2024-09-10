using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaOrders.Application.Events
{
    public class OrderCreated
    {

        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Amount { get; set; }
    }


}
