﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaOrders.Application.Events
{
    public class CancelOrder
    {
        public Guid OrderId { get; set; }
    }

}
