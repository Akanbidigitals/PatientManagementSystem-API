using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem_API.DataAccess.Interface;
using PatientManagementSystem_API.Domain.DTos.Patient;
using PatientManagementSystem_API.Domain.Models;

namespace PatientManagementSystem_API.Controllers
{
    [ApiController]
    [Route("/api/patients")]
    public class PatientController(IPatientRepository patientRepository) : Controller
    {
        //Create 
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient([FromBody]CreatePatientDTO _dto)
        {
            var patient = await patientRepository.AddPatient(_dto);
            return Ok(patient);

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
        {
            var patients = await patientRepository.GetAllPatient();
            return Ok(patients);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetbyId([FromRoute] int id)
        {
            var patient = await patientRepository.GetPatientById(id);
            return Ok(patient);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<Patient>>UpdatePatientById([FromRoute] int id, [FromBody] UpdatePatientDTO updateDto)
        {
            var updatePatient = await patientRepository.UpdatePatient(id,updateDto);
            return Ok(updatePatient);
        }

        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete([FromRoute]int id)
        {
             await patientRepository.DeletePatient(id);
            return NoContent();
        }


    }
}
