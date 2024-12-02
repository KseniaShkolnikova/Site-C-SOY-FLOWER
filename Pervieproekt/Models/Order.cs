using System;
using System.Collections.Generic;

namespace Pervieproekt.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public int PosOrdersId { get; set; }

    public DateTime Datee { get; set; }

    public decimal Summ { get; set; }

    public int PaymentTypeId { get; set; }

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual PosOrder PosOrders { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
