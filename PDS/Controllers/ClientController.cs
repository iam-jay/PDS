using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PDS.Data;
using PDS.Models;

namespace PDS.Controllers
{
    [Route("cleint")]
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
        public async Task<ActionResult<String>> OnBoardPatient(Client client)
        {
            _logger.LogInformation("Inside client add");
            ClientData data = new ClientData();
            data.addClientDetails(client);
            _dataContext.ClientData.Add(data);
            await _dataContext.SaveChangesAsync();
            return Ok(data.GUID);
        }
    }
}
