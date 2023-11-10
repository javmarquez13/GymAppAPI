using System;
using System.Collections.Generic;

namespace GymAppAPI.Models;

public partial class Attendance
{
    public long IdAttendance { get; set; }

    public long IdClient { get; set; }

    public DateTime Date { get; set; }

    public DateTime StartingTime { get; set; }

    public DateTime FinishingTime { get; set; }

    public virtual Client IdClientNavigation { get; set; } = null!;
}
