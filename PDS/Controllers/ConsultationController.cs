using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDS.Data;
using PDS.Models;

namespace PDS.Controllers
{
    [Route("consultation")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private readonly ILogger<ConsultationController> _logger;
        private DataContext _dataContext = new DataContext();
        public ConsultationController(ILogger<ConsultationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("{pui}")]
        [Authorize]
        public async Task<ActionResult<String>> AddConsultation(string pui, Consultation consultation)
        {
            _logger.LogInformation("Inside consultation add");
            PatientData patientData = await _dataContext.PatientData.OfType<PatientData>().Where(x => x.PUI == pui).FirstAsync();
            if (patientData == null)
            {
                return NotFound("No such patient exits");
            }
            var claims = HttpContext?.User.Claims;
            string UserId = claims.First(x => x.Type == "UserID").Value;
            string UserType = claims.First(x => x.Type == "TokenType").Value;
            string Name = claims.First(x => x.Type == "DisplayName").Value;
            ConsultationData data = new ConsultationData();
            consultation.ClientId = UserId;
            consultation.HospitalName = Name;
            consultation.PatientId = patientData.GUID;
            data.addConsultationDetails(consultation);
            _dataContext.ConsultationData.Add(data);
            await _dataContext.SaveChangesAsync();
            return Ok(data.GUID);
        }

    }
}
