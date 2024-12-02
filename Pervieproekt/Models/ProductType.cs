using System;
using System.Collections.Generic;

namespace Pervieproekt.Models;

public partial class ProductType
{
    public int IdProductType { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Catalogg> Cataloggs { get; set; } = new List<Catalogg>();
}
