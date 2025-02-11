using PatientManagementSystem_API.Domain.DTos.Patient;
using PatientManagementSystem_API.Domain.Models;

namespace PatientManagementSystem_API.DataAccess.Interface
{
    public interface IPatientRepository
    {
        Task<Patient> AddPatient(CreatePatientDTO createPatientDTO);
        Task<string> DeletePatient(int id); 
        Task<Patient> GetPatientById(int id);

        Task<Patient> UpdatePatient(int id,UpdatePatientDTO updatePatientDTO);
        Task<IEnumerable<Patient>> GetAllPatient();

    }
}
