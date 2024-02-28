using API_Dto;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> GetStudents(int index = 0 , int count = 10)
        {
            if (_studentService == null)
            {
                return StatusCode(500);
            }
            var data = await _studentService.GetStudents(index, count);
            if (data == null)       
            {
                return NoContent();
            }
            else
            {
                return Ok(data);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(long id)
        {
            if (_studentService == null)
            {
                return StatusCode(500);
            }
            var book = await _studentService.GetStudentById(id);
            if(book == null)
            {
                return NotFound();
            }
            else return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] StudentDto student)
        {
            if (_studentService == null)
            {
                return StatusCode(500);
            }
            var studentDto = await _studentService.PostStudent(student);
            if (studentDto == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(studentDto);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutStudent(long id, [FromBody] StudentDto student)
        {
            if (_studentService == null)
            {
                return StatusCode(500);
            }
            var studentDto = await _studentService.PutStudent(id, student);
            if (studentDto == null) return NotFound();
            else return Ok(studentDto);           
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            if (_studentService == null)
            {
                return StatusCode(500);
            }
            else
            {
                bool b = await _studentService.DeleteStudent(id);
                if (b)
                {
                    return Ok(b);
                }
                else
                { return NotFound(); }
            }
        }


    }
}
