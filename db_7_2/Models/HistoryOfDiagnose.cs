using System;
using System.Collections.Generic;

namespace db_7_2.Models;

public partial class HistoryOfDiagnose
{
    public long Id { get; set; }

    public DateOnly DateOfDetection { get; set; }

    public long ElectronicCardId { get; set; }

    public long DiagnoseId { get; set; }

    public virtual Diagnose Diagnose { get; set; } = null!;

    public virtual ElectronicCard ElectronicCard { get; set; } = null!;
}
