using System.ComponentModel.DataAnnotations;

namespace db_7_2.Models
{

  
    public class StatisticsOfTreatedCasesDTO
    {
        public string DiagnoseName { get; set; } = null!;
        public int NumberOfCases { get; set; }

    }
}
