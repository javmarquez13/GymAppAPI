using System;
using System.Collections.Generic;

namespace GymAppAPI.Models;

public partial class Payment
{
    public long IdPayment { get; set; }

    public long IdClient { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;
}
