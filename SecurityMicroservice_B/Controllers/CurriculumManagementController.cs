using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecurityMicroservice_B.Models;

namespace SecurityMicroservice_B.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CurriculumManagementController : ControllerBase
    {

        /// <summary>
        /// CreateCurriculum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public CreateCurriculumResponse CreateCurriculum([FromBody] CreateCurriculum value)
        {
            return new CreateCurriculumResponse() { Message = value.CurriculumName + " Curriculum created successfully" };
        }

        /// <summary>
        /// Delete curriculum
        /// </summary>
        /// <param name="packageName"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public bool Delete(string curriculumName)
        {
            return true;
        }
    }

    public class CreateCurriculumResponse
    {
        public string Message { get; set; }
    }
}
