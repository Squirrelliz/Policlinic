using System;
using System.Collections.Generic;

namespace db_7_2.Models;

public partial class Qualification
{
    public long Id { get; set; }

    public string Specialization { get; set; } = null!;

    public string Education { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int Experience { get; set; }

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
