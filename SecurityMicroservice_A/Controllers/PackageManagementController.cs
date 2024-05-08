using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityMicroservice_A.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SecurityMicroservice_A.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PackageManagementController : ControllerBase
    {

        // POST api/<PackageManagementController>
        [HttpPost("create")]
        public CreatePackageResponse CreatePackage([FromBody] CreatePackage value)
        {
            return new CreatePackageResponse() { Message = value.PackageName + " Package created successfully" };
        }

        // DELETE api/<PackageManagementController>/5
        [HttpDelete("delete")]
        public bool Delete(string packageName)
        {
            return true;
        }
    }

    public class CreatePackageResponse
    {
        public string Message { get; set; }
    }
}
