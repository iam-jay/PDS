using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDS.Data;
using PDS.Helpers;
using PDS.Models;

namespace PDS.Controllers
{
    [Route("consent")]
    [ApiController]
    public class ConsentFlowController: ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private DataContext _dataContext = new DataContext();

        public ConsentFlowController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{pui}")]
        [Authorize]
        public async Task<ActionResult<ConsentResponseData>> GetOTP(string pui)
        {
            var claims = HttpContext?.User.Claims;
            string UserId = claims.First(x => x.Type == "UserID").Value;
            string UserType = claims.First(x => x.Type == "TokenType").Value;
            PatientData patientData = await _dataContext.PatientData.OfType<PatientData>().Where(x => x.PUI == pui).FirstAsync();
            if(patientData == null)
            {
                return NotFound();
            }
            Random generator = new Random();
            ConsentResponseData consentResponseData = new ConsentResponseData();
            consentResponseData.PUI = pui;
            consentResponseData.OTP = generator.Next(100000, 1000000);
            consentResponseData.ClientId = UserId;
            _dataContext.ConsentResponseData.Add(consentResponseData);
            await _dataContext.SaveChangesAsync();
            return Ok(consentResponseData);
        }

        [HttpPost("grant")]
        [Authorize]
        public async Task<ActionResult<JWTTokenResponse>> VerifyConsent(ConsentGrantModel consentGrantModel)
        {
            var claims = HttpContext?.User.Claims;
            string UserId = claims.First(x => x.Type == "UserID").Value;
            string UserType = claims.First(x => x.Type == "TokenType").Value;
            PatientData patientData = await _dataContext.PatientData.OfType<PatientData>().Where(x => x.PUI == consentGrantModel.PUI).FirstAsync();
            ClientData clientData = await _dataContext.ClientData.OfType<ClientData>().Where(x => x.GUID == UserId).FirstAsync();
            if (patientData == null || clientData == null)
            {
                return NotFound();
            }
            ConsentResponseData consentResponseData = await _dataContext.ConsentResponseData.OfType<ConsentResponseData>().Where(x => x.ClientId == UserId && x.PUI == consentGrantModel.PUI).FirstAsync();
            if(consentResponseData != null)
            {
                if(consentGrantModel.OTP == consentResponseData.OTP)
                {
                    AuthorisedData authorisedData = new AuthorisedData();
                    authorisedData.CleintId = UserId;
                    authorisedData.PatientId = patientData.GUID;
                    _dataContext.ConsentResponseData.Remove(consentResponseData);
                    _dataContext.AuthorisedData.Add(authorisedData);
                    await _dataContext.SaveChangesAsync();
                    return Ok("Access granted");
                }
            }
            return BadRequest("Wrong OTP");
        }
    }
}
