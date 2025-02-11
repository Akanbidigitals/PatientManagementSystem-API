namespace PatientManagementSystem_API.Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Email { get; set; }
        public string? DateOfBirth {  get; set; }
        public string? PhoneNumber { get; set; }
        public string?Address { get; set; } 
        public string? UpdatedAt { get; set; }
        public string RegisteredAt { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
        public bool IsDeleted { get; set; } = false;
        public List<PatientRecord> PatientRecords { get; set; } = [];

    }
}
