using Microsoft.EntityFrameworkCore;
using PatientManagementSystem_API.DataAccess.DataContext;
using PatientManagementSystem_API.DataAccess.Interface;
using PatientManagementSystem_API.Domain.DTos.Record;
using PatientManagementSystem_API.Domain.Models;
using PatientManagementSystem_API.Exceptions;

namespace PatientManagementSystem_API.DataAccess.Repository
{
    public class PatientRecordRepository(ILogger<PatientRecordRepository> logger, ApplicationContext _ctx) : IPatientRecord
    {
        public async Task<PatientRecord> CreateRecord(int patientId, CreateRecordDTO createRecordDTO)
        {  

            
            logger.LogInformation($"Creating record for patient with Id:{patientId}");
            var patient = await _ctx.Patients.FirstOrDefaultAsync(x=>x.Id == patientId);
            if (patient == null) throw new  NotFoundException($"Patient Id : {patientId} does not exist in the datebase");
            var patientRecord = new PatientRecord
            {
                Diagnosis = createRecordDTO.Diagnosis,
                PatientId = patientId,
                Treatment = createRecordDTO.Treatment,
                CreatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                Patient = patient,
                UpdatedAt = null
              
                
            };

            await _ctx.PatientRecords.AddAsync(patientRecord);
            await _ctx.SaveChangesAsync();
            return patientRecord;
        }

        public async Task<List<PatientRecord>> GetAllPatientRecord(int patientId)
        {
            logger.LogInformation($"Getting all records for patientId:{patientId}");
            var patient = await _ctx.Patients.Include(r => r.PatientRecords).FirstOrDefaultAsync(x => x.Id == patientId);
            if (patient == null) throw new NotFoundException($"Patient Id : {patientId} does not exist in the datebase");
            return patient.PatientRecords;
        }

        public async Task<PatientRecord> GetPatientRecordById(int patientId, int recordId)
        {
            logger.LogInformation($"Getting record for patientId:{patientId} with recordId : {recordId}");
            var patient = await _ctx.Patients.Include(x => x.PatientRecords).FirstOrDefaultAsync(x => x.Id == patientId);
            if (patient == null) throw new NotFoundException($"PatientId : {patientId} with recordId : {recordId} does not exist");

            var record = patient.PatientRecords.FirstOrDefault(x => x.Id == recordId);
            if(record == null) throw new NotFoundException($"Record Id : {recordId} does not exist in the datebase");
            return record;
        }

        public async Task<PatientRecord> UpdateRecord(int patientId, int recordId, CreateRecordDTO createRecordDTO)
        {
            logger.LogInformation($"Updating record for patientId:{patientId} with recordId : {recordId}");
            var patient = await _ctx.Patients.Include(x => x.PatientRecords).FirstOrDefaultAsync(x => x.Id == patientId);
            if (patient == null) throw new NotFoundException($"Patient Id : {patientId} with recordId :{recordId} does  not exist");

            var record = patient.PatientRecords.FirstOrDefault(x => x.Id == recordId);
            if (record == null) throw new NotFoundException($"Record Id : {recordId} does not exist in the datebase");
            
            record.PatientId = patientId;
            record.Id = recordId;
            record.Diagnosis = createRecordDTO.Diagnosis ?? record.Diagnosis;
            record.UpdatedAt = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            

             _ctx.PatientRecords.Update(record);
            await _ctx.SaveChangesAsync();
            return record;
              
        }
    }
}
