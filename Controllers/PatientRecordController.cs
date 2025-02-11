using Microsoft.AspNetCore.Mvc;
using PatientManagementSystem_API.DataAccess.Interface;
using PatientManagementSystem_API.Domain.DTos.Record;
using PatientManagementSystem_API.Domain.Models;

namespace PatientManagementSystem_API.Controllers
{

    [ApiController]
    [Route("/api/patients/{patientId}/records")]
    public class PatientRecordController(IPatientRecord patientRecord) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreatePatientRecord([FromRoute] int patientId, CreateRecordDTO _dto)
        {
           
            var addRecord = await patientRecord.CreateRecord(patientId, _dto);
            return Ok(addRecord);
        }

        [HttpGet("{recordId}")]
        public async Task<ActionResult<PatientRecord>> GetRecordById([FromRoute] int patientId,int recordId)
        {
            var getRecord = await patientRecord.GetPatientRecordById(patientId, recordId);
            return Ok(getRecord);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientRecord>>> GetAllPatientRecords([FromRoute] int patientId)
        {
            var getAllRecords = await patientRecord.GetAllPatientRecord(patientId);
            return Ok(getAllRecords);
        }

        [HttpPut("{recordId}")]
        public async Task<ActionResult<PatientRecord>> UpdateRecord([FromRoute]int  patientId,int recordId,CreateRecordDTO _dto)
        {
            var updatedRecord = await patientRecord.UpdateRecord(patientId, recordId, _dto);
            return Ok(updatedRecord);
        }
    }
}
