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

        [HttpGet("{pui}/{clientId}")]
        [Authorize]
        public async Task<ActionResult<ConsentResponseData>> GetOTP(string pui, string clientId)
        {
            PatientData patientData = await _dataContext.PatientData.OfType<PatientData>().Where(x => x.PUI == pui).FirstAsync();
            if(patientData == null)
            {
                return NotFound();
            }
            Random generator = new Random();
            ConsentResponseData consentResponseData = new ConsentResponseData();
            consentResponseData.PUI = pui;
            consentResponseData.OTP = generator.Next(100000, 1000000);
            consentResponseData.ClientId = clientId;
            _dataContext.ConsentResponseData.Add(consentResponseData);
            await _dataContext.SaveChangesAsync();
            return Ok(consentResponseData);
        }

        [HttpPost("grant")]
        [Authorize]
        public async Task<ActionResult<JWTTokenResponse>> VerifyConsent(ConsentGrantModel consentGrantModel)
        {
            PatientData patientData = await _dataContext.PatientData.OfType<PatientData>().Where(x => x.PUI == consentGrantModel.PUI).FirstAsync();
            ClientData clientData = await _dataContext.ClientData.OfType<ClientData>().Where(x => x.GUID == consentGrantModel.ClientId).FirstAsync();
            if (patientData == null || clientData == null)
            {
                return NotFound();
            }
            ConsentResponseData consentResponseData = await _dataContext.ConsentResponseData.OfType<ConsentResponseData>().Where(x => x.ClientId == consentGrantModel.ClientId && x.PUI == consentGrantModel.PUI).FirstAsync();
            if(consentResponseData != null)
            {
                if(consentGrantModel.OTP == consentResponseData.OTP)
                {
                    TokenData tokenData = new TokenData();
                    tokenData.Id = consentResponseData.GUID;
                    tokenData.DisplayName = "PatientClient";
                    tokenData.TokenType = "PatientClient";
                    _dataContext.ConsentResponseData.Remove(consentResponseData);
                    return Ok(new JWTTokenResponse { Token = JwtTokenhandler.generateJwtToken(tokenData) });
                }
            }
            return BadRequest("Wrong OTP");
        }
    }
}
