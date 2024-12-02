using System;
using System.Collections.Generic;

namespace Pervieproekt.Models;

public partial class Review
{
    public int IdReviews { get; set; }

    public int UserrId { get; set; }

    public string Review1 { get; set; } = null!;

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User Userr { get; set; } = null!;
}
