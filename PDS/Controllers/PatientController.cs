using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using PDS.Data;
using PDS.DTO;
using PDS.Helpers;
using PDS.Models;

namespace PDS.Controllers
{
    [Route("patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly ILogger<PatientController> _logger;
        private DataContext _dataContext = new DataContext();

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }
        

        [HttpPost("GenerateToken")]
        public ActionResult<JWTTokenResponse> GenerateToken()
        {
            _logger.LogInformation("Inside generate token");
            TokenData tokenData = new TokenData();
            tokenData.Id = Guid.NewGuid().ToString();
            tokenData.DisplayName = "Jay";
            tokenData.TokenType = "User";
            return Ok(new JWTTokenResponse { Token = JwtTokenhandler.generateJwtToken(tokenData) });
        }
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

        [HttpGet("{pui}")]
        public async Task<ActionResult<PatientData>> getPatientDetailsUsingPUI(string pui)
        {
            _logger.LogInformation("Inside patient get");
            PatientData patientData = await _dataContext.PatientData.OfType<PatientData>().Where(x => x.PUI == pui).FirstAsync();
            return Ok(patientData);
        }

        [HttpGet("{pui}/consultation")]
        [Authorize]
        public async Task<ActionResult<PatientData>> getPatientConsultations(string pui)
        {
            _logger.LogInformation("Inside patient get");
            PatientData patientData = await _dataContext.PatientData.OfType<PatientData>().Where(x => x.PUI == pui).FirstAsync();
            List<ConsultationData> consultations = await _dataContext.ConsultationData.Where(x =>x.PatientId == patientData.GUID).ToListAsync();
            PatientConsultations patientConsultations = new PatientConsultations();
            patientConsultations.consultations = consultations;
            patientConsultations.patientData = patientData;
            return Ok(patientConsultations);

        }
    }
}
