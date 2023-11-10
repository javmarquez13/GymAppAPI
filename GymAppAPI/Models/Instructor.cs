using System;
using System.Collections.Generic;

namespace GymAppAPI.Models;

public partial class Instructor
{
    public int IdInstructors { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Speciality { get; set; } = null!;
}
