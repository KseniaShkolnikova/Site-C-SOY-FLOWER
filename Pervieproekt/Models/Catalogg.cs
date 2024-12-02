using System;
using System.Collections.Generic;

namespace Pervieproekt.Models;

public partial class Catalogg
{
    public int IdCatalog { get; set; }

    public string Namee { get; set; } = null!;

    public decimal Price { get; set; }

    public string Img { get; set; } = null!;

    public string? Information { get; set; }

    public int ProductTypeId { get; set; }

    public virtual ICollection<PosOrder> PosOrders { get; set; } = new List<PosOrder>();

    public virtual ProductType ProductType { get; set; } = null!;
}
