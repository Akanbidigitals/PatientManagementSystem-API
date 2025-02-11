using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PatientManagementSystem_API.DataAccess.DataContext;
using PatientManagementSystem_API.DataAccess.Interface;
using PatientManagementSystem_API.Domain.DTos.Patient;
using PatientManagementSystem_API.Domain.Models;
using PatientManagementSystem_API.Exceptions;

namespace PatientManagementSystem_API.DataAccess.Repository
{
    public class PatientRepository(ILogger<PatientRepository> logger,ApplicationContext _ctx,IMemoryCache cache) : IPatientRepository
    {
       
        public async Task<Patient> AddPatient(CreatePatientDTO createPatientDTO)
        {
            var checkPatient = await _ctx.Patients.AnyAsync(x => x.Email == createPatientDTO.Email);
            if (checkPatient)
            {
                throw new AlreadyExistException($"Email ${createPatientDTO.Email} already exist in the datebase");
            }

            logger.LogInformation("Creating new patient");
            var newPatient = new Patient
            {
               FirstName = createPatientDTO.FirstName,
               Email = createPatientDTO.Email,
               LastName = createPatientDTO.LastName,
               Address = createPatientDTO.Address,
               DateOfBirth = createPatientDTO.DateOfBirth,
               PhoneNumber = createPatientDTO.PhoneNumber,
            };

            await _ctx.Patients.AddAsync(newPatient);
            await _ctx.SaveChangesAsync();
            return newPatient;
        }

        public async Task<string> DeletePatient(int id)
        {
            logger.LogWarning($"Deleting patient with the Id :{id}");
            var patient = await _ctx.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if(patient  == null)
            {
                throw new NotFoundException($"Patient with Id:{id} does not exist in the DateBase");
            }
            patient.IsDeleted = true;
            _ctx.Patients.Update(patient);
            await _ctx.SaveChangesAsync();
            return "Patient sucessfully deleted";
        }

        public async Task<IEnumerable<Patient>> GetAllPatient()
        {
            logger.LogInformation("Getting All Patients in the database");

            var patientCacheKey = "AllPatients";
            var patientsCache = cache.TryGetValue(patientCacheKey, out List<Patient>? patients);
            if(patientsCache == false)
            {
                patients = await _ctx.Patients.ToListAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(1) // Cache for 3min
                };

                cache.Set(patientCacheKey, patients, cacheEntryOptions);
            }


            return patients!;
        }

        public async Task<Patient> GetPatientById(int id)
        {
            logger.LogInformation($"Getting patient with Id :{id}");

            //Using Caching from GetAllMehods
           // var patients = await GetAllPatient();
           // var patient = patients.Where(x => x.Id == id).FirstOrDefault();
            //--------------------------------------------------

            var patient = await _ctx.Patients.Include(r => r.PatientRecords).FirstOrDefaultAsync(x => x.Id == id); //Using eager loading mode  to also access patientRecords.
            if (patient is null) throw new NotFoundException($"Patient with Id:{id} does not exist in the DateBase");
            return patient;
        }

        public async Task<Patient> UpdatePatient(int id,UpdatePatientDTO updatePatientDTO)
        {
            logger.LogInformation($"Updating patient record for Id : {id}");
            var patient = await _ctx.Patients.FirstOrDefaultAsync(x => x.Id == id);
            if (patient == null)
            {
                throw new NotFoundException($"Patient with Id:{id} does not exist in the DateBase");
            }

            patient.Id = id;
            patient.FirstName = updatePatientDTO.FirstName ?? patient.FirstName;
            patient.LastName = updatePatientDTO.LastName ?? patient.LastName;
            patient.Email = updatePatientDTO.Email ?? patient.Email;    
            patient.Address = updatePatientDTO.Address ?? patient.Address;
            patient.UpdatedAt = DateTime.Now.ToString("dd-MM-yyyy");
            patient.PhoneNumber = updatePatientDTO.PhoneNumber ?? patient.PhoneNumber;

            _ctx.Patients.Update(patient);
            await _ctx.SaveChangesAsync();
            return patient;

        }
    }
}
