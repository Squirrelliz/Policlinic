using System;
using System.Collections.Generic;

namespace db_7_2.Models;

public partial class Patient
{
    public long Id { get; set; }

    public string Fcs { get; set; } = null!;

    public long ElectronicCardId { get; set; }

    public long ContactDataId { get; set; }

    public DateOnly DateOfBirth { get; set; }

    public virtual ContactDatum ContactData { get; set; } = null!;

    public virtual ElectronicCard ElectronicCard { get; set; } = null!;
}
