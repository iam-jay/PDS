using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDS.Data;
using PDS.Helpers;
using PDS.Models;

namespace PDS.Controllers
{
    [Route("client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private DataContext _dataContext = new DataContext();
        public ClientController(ILogger<PatientController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<String>> OnBoardClient(Client client)
        {
            _logger.LogInformation("Inside client add");
            ClientData data = new ClientData();
            data.addClientDetails(client);
            _dataContext.ClientData.Add(data);
            await _dataContext.SaveChangesAsync();
            return Ok(data.GUID);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginModel login)
        {
            _logger.LogInformation("Inside user login");
            ClientData clientData = await _dataContext.ClientData.OfType<ClientData>().Where(x => x.Email == login.UserEmail && x.Password == login.Password).FirstAsync();
            if (clientData == null)
            {
                return BadRequest("User Email or Password incorrect");
            }
            TokenData tokenData = new TokenData();
            tokenData.Id = clientData.GUID;
            tokenData.DisplayName = clientData.ClientName;
            tokenData.TokenType = "Client";
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.UserID = clientData.GUID;
            loginResponse.AccessToken = JwtTokenhandler.generateJwtToken(tokenData);
            AuthenticationData authenticationData = new AuthenticationData();
            authenticationData.AccessToken = loginResponse.AccessToken;
            _dataContext.AuthenticationData.Add(authenticationData);
            await _dataContext.SaveChangesAsync();
            loginResponse.UserType = "Client";
            authenticationData.UserType = "Client";
            authenticationData.UserId = clientData.GUID;
            return Ok(loginResponse);
        }
    }
}
