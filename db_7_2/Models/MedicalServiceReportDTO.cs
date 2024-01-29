namespace db_7_2.Models
{
    public class MedicalServiceReportDTO
    {
        public long Id { get; set; }
        public string ServiceName { get; set; } = null!;
        
        public string Result { get; set; } = null!;
       
        public string PatientFcs { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public DateTime DateTime { get; set; }
        public string Specialization { get; set; } = null!;
        public string DoctorFcs { get; set; } = null!;
    }
}
