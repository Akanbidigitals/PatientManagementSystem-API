namespace PatientManagementSystem_API.Domain.DTos.Patient
{
    public class CreatePatientDTO
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string DateOfBirth { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Address { get; set; } = default!;
    }
}
