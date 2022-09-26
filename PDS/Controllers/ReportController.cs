using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using PDS.Data;
using ConfigurationManager = PDS.Helpers.ConfigurationManager;

namespace PDS.Controllers
{
    [Route("consultation")]
    [ApiController]
    public class ReportController : ControllerBase
    {

        private DataContext _dataContext = new DataContext();

        [HttpPost("reports")]
        [Authorize]
        public async Task<IActionResult> UploadReports(string consultationId, List<IFormFile>  files)
        {
            ConsultationData consultationData = await _dataContext.ConsultationData.OfType<ConsultationData>().Where(x => x.GUID == consultationId).FirstAsync();
            if(consultationData == null)
            {
                return NotFound("Invalid consultation details");
            }
            List<string> urls = new List<string>();
            files.ForEach(file =>
            {
                string systemFileName = file.FileName;
                string blobstorageconnection = ConfigurationManager.AppSetting["BlobStorage:BlobConnectionString"];
                // Retrieve storage account from connection string.    
                CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(blobstorageconnection);
                // Create the blob client.    
                CloudBlobClient blobClient = cloudStorageAccount.CreateCloudBlobClient();
                // Retrieve a reference to a container.    
                CloudBlobContainer container = blobClient.GetContainerReference(ConfigurationManager.AppSetting["BlobStorage:BlobContainerName"]);
                // This also does not make a service call; it only creates a local object.    
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(systemFileName);
                // blockBlob.UploadFromFileAsync(files);
                using (var stream = new MemoryStream())
                {
                    file.CopyTo(stream);
                    blockBlob.UploadFromStreamAsync(stream).Wait();
                    urls.Add(blockBlob.Uri.AbsoluteUri);
                }
            });
            List<string> reports = consultationData.Reports.Concat(urls).ToList();
            consultationData.Reports = reports;
            _dataContext.ConsultationData.Update(consultationData);
            await _dataContext.SaveChangesAsync();
            return Ok("Reports updated Successfully");
        }
    }
}
