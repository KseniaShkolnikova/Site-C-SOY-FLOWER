using System;
using System.Collections.Generic;

namespace Pervieproekt.Models;

public partial class PosOrder
{
    public int IdPosOrders { get; set; }

    public string UserrId { get; set; } = null!;
    
    public int CatalogId { get; set; }

    public int Countt { get; set; }

    public decimal Price { get; set; }

    public virtual Catalogg Catalog { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
