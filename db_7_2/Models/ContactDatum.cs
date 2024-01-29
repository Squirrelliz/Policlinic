using System;
using System.Collections.Generic;

namespace db_7_2.Models;

public partial class ContactDatum
{
    public long Id { get; set; }

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PassportData { get; set; } = null!;

    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
