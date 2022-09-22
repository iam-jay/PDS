using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDS.Data;
using PDS.Models;

namespace PDS.Controllers
{
    [Route("patientdetails")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly ILogger<PatientController> _logger;
        private DataContext _dataContext = new DataContext();
        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }
        /*[HttpGet]
        public async Task<ActionResult<List<PatientDetails>>> Get()
        {
            _logger.LogDebug("Inside patient get");
            return Ok();
        }*/


        [HttpPost]
        public async Task<ActionResult<String>> OnBoardPatient(PatientProfile patientProfile)
        {
            _logger.LogInformation("Inside patient add");
            PatientData data = new PatientData();
            data.addPatientDetails(patientProfile);
            _dataContext.PatientData.Add(data);
            await _dataContext.SaveChangesAsync();
            return Ok(data.GUID);
        }

        /*[HttpPut]
        public async Task<ActionResult<PatientDetails>> UpdatePatientDetails(PatientDetails patientDetails)
        {
            _logger.LogInformation("Inside patient update");
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<List<PatientDetails>>> DeletePatientDetails(PatientDetails patientDetails)
        {
            _logger.LogInformation("Inside patient delete");
            return Ok();
        }*/
    }
}
