using System;
using System.Collections.Generic;

namespace db_7_2.Models;

public partial class MedicalService
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<HistoryOfMedicalService> HistoryOfMedicalServices { get; set; } = new List<HistoryOfMedicalService>();
}
