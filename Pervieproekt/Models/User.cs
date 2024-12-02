using System;
using System.Collections.Generic;

namespace Pervieproekt.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? Namee { get; set; }

    public string Loginn { get; set; } = null!;

    public string Passwordd { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
