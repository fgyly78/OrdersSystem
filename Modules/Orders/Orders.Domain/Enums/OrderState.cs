using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Domain.Enums
{
    public enum OrderState
    {
        Created,
        Paid,
        Completed,
        Cancelled
    }
}
