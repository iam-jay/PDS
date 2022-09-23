using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDS.Data;
using PDS.Models;

namespace PDS.Controllers
{
    [Route("consultation")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private DataContext _dataContext = new DataContext();
        public ConsultationController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<String>> OnBoardPatient(Consultation consultation)
        {
            _logger.LogInformation("Inside client add");
            ConsultationData data = new ConsultationData();
            data.addConsultationDetails(consultation);
            _dataContext.ConsultationData.Add(data);
            await _dataContext.SaveChangesAsync();
            return Ok(data.GUID);
        }

    }
}
