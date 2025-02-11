using PatientManagementSystem_API.Domain.DTos.Record;
using PatientManagementSystem_API.Domain.Models;

namespace PatientManagementSystem_API.DataAccess.Interface
{
    public interface IPatientRecord
    {
        Task<PatientRecord> CreateRecord(int patientId , CreateRecordDTO createRecordDTO);
        Task<PatientRecord> UpdateRecord(int patientId,int recordId, CreateRecordDTO createRecordDTO);
        Task<List<PatientRecord>> GetAllPatientRecord(int patientId);
        Task<PatientRecord> GetPatientRecordById(int patientId, int recordId);

        
    }
}
