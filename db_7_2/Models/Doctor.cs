using System;
using System.Collections.Generic;

namespace db_7_2.Models;

public partial class Doctor
{
    public long Id { get; set; }

    public string Fcs { get; set; } = null!;

    public long QualificationId { get; set; }

    public long ContactDataId { get; set; }

    public virtual ContactDatum ContactData { get; set; } = null!;

    public virtual ICollection<HistoryOfMedicalService> HistoryOfMedicalServices { get; set; } = new List<HistoryOfMedicalService>();

    public virtual Qualification Qualification { get; set; } = null!;
}
