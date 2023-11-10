using System;
using System.Collections.Generic;

namespace GymAppAPI.Models;

public partial class User
{
    public long IdUser { get; set; }

    public string Email { get; set; } = null!;

    public string NickName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
