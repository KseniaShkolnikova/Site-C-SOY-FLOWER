using System;
using System.Collections.Generic;

namespace Pervieproekt.Models;

public partial class PaymentType
{
    public int IdPaymentType { get; set; }

    public string PaymentType1 { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
