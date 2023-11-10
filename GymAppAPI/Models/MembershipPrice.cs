using System;
using System.Collections.Generic;

namespace GymAppAPI.Models;

public partial class MembershipPrice
{
    public long IdMembership { get; set; }

    public string MembershipType { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;
}
