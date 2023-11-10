using System;
using System.Collections.Generic;

namespace GymAppAPI.Models;

public partial class Client
{
    public long IdClient { get; set; }

    public long IdUser { get; set; }

    public string? Email { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string Address { get; set; } = null!;

    public long Phone { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string MembershipType { get; set; } = null!;

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual User IdUserNavigation { get; set; } = null!;

    public virtual ICollection<MembershipStatus> MembershipStatuses { get; set; } = new List<MembershipStatus>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
