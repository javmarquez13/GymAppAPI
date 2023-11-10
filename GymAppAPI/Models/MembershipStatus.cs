using System;
using System.Collections.Generic;

namespace GymAppAPI.Models;

public partial class MembershipStatus
{
    public long IdPayment { get; set; }

    public long IdClient { get; set; }

    public DateTime PaymentDate { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool Active { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;
}
