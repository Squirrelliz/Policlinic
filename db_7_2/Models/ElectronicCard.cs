using System;
using System.Collections.Generic;

namespace db_7_2.Models;

public partial class ElectronicCard
{
    public long Id { get; set; }

    public DateOnly DateOfCreation { get; set; }

    public string MhiPolicy { get; set; } = null!;

    public virtual ICollection<HistoryOfDiagnose> HistoryOfDiagnoses { get; set; } = new List<HistoryOfDiagnose>();

    public virtual ICollection<HistoryOfMedicalService> HistoryOfMedicalServices { get; set; } = new List<HistoryOfMedicalService>();

    public virtual ICollection<Patient> Patients { get; set; } = new List<Patient>();
}
