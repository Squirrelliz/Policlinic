using System;
using System.Collections.Generic;

namespace db_7_2.Models;

public partial class HistoryOfMedicalService
{
    public long Id { get; set; }

    public string Result { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public long ElectronicCardId { get; set; }

    public long DoctorId { get; set; }

    public long MedicalServiceId { get; set; }

    public StatusOfMedicalService Status { get; set; }

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual ElectronicCard ElectronicCard { get; set; } = null!;

    public virtual MedicalService MedicalService { get; set; } = null!;
}
