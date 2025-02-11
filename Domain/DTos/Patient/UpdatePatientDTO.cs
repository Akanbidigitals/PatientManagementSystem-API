namespace PatientManagementSystem_API.Domain.DTos.Patient
{
    public class UpdatePatientDTO : CreatePatientDTO
    {
    
        public string? UpdatedAt { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
        
    }
}
