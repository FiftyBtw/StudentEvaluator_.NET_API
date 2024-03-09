using API_Dto;
using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API_EF.Controllers.V1
{
    /// <summary>
    ///  Controller for students
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService<StudentDto> _studentService;

        public StudentsController(IStudentService<StudentDto> studentService)
        {
            _studentService = studentService;
        }

        /// <summary>
        ///  Get all students
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Get a student by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Add a student
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Update a student
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Delete a student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
