using System;
using System.Collections.Generic;

namespace GymAppAPI.Models;

public partial class Admin
{
    public int IdAdmin { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;
}
