using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {   //HTTP GET
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] arrStudentNames = new string[] { "1", "2", "3" };

            return Ok(arrStudentNames);
        }
    }
}
