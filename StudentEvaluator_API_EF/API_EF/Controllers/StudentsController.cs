using API_Dto;
using Microsoft.AspNetCore.Mvc;

namespace API_EF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents(int index, int count)
        {
            if (_studentService == null)
            {
                return StatusCode(500);
            }
            var data = await _studentService.GetStudents(index, count);
            if (data == null)       
            {
                return StatusCode(204);
            }
            else
            {
                return Ok(data);
            }

        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetStudentById(long id)
        {
            if (_studentService == null)
            {
                return StatusCode(500);
            }
            var book = await _studentService.GetStudentById(id);
            if(book == null)
            {
                return StatusCode(201);
            }
            else return Ok(book);
        }

    }
}
