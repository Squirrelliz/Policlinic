using System;
using System.Collections.Generic;

namespace db_7_2.Models;

public partial class Diagnose
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<HistoryOfDiagnose> HistoryOfDiagnoses { get; set; } = new List<HistoryOfDiagnose>();
}
