using API_Dto;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API_EF.Controllers.V1
{
    /// <summary>
    ///  Controller for lessons
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LessonsController : ControllerBase
    {

        private readonly ILessonService<LessonDto,LessonReponseDto>  _lessonService;

        public LessonsController(ILessonService<LessonDto, LessonReponseDto> lessonService)
        {
            _lessonService = lessonService;
        }

        /// <summary>
        ///  Get all lessons
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLessons(int index=0, int count=10)
        {
            if (_lessonService == null)
            {
                return StatusCode(500);
            }
            var data = await _lessonService.GetLessons(index, count);
            if (data == null) return NoContent();
            else return Ok(data);
        }

        /// <summary>
        ///  Get a lesson by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetLessonById(long id)
        {
            if (_lessonService == null)
            {
                return StatusCode(500);
            }
            var lesson = await _lessonService.GetLessonById(id);
            if(lesson == null)
            {
                return NotFound();
            }
            else return Ok(lesson);
        }

        /// <summary>
        ///  Get all lessons of a teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("teacher/{id}")]
        public async Task<IActionResult> GetLessonsByTeacherId(long id,int index = 0, int count = 10)
        {
            if (_lessonService == null)
            {
                return StatusCode(500);
            }
            var data = await _lessonService.GetLessonsByTeacherId(id,index,count);
            if (data == null) return NoContent();
            else return Ok(data);
        }

        /// <summary>
        ///  Add a lesson
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostLesson([FromBody] LessonDto lesson)
        {
            if (_lessonService == null)
            {
                return StatusCode(500);
            }
            var lessonDto = await _lessonService.PostLesson(lesson);
            if (lessonDto == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(lessonDto);
            }
        }

        /// <summary>
        ///  Update a lesson
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lesson"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutLesson(long id, [FromBody] LessonDto lesson)
        {
            if (_lessonService == null)
            {
                return StatusCode(500);
            }
            var lessonDto = await _lessonService.PutLesson(id,lesson);
            if (lessonDto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(lessonDto);
            }
        }

        /// <summary>
        ///  Delete a lesson
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteLesson(long id)
        {
            if (_lessonService == null)
            {
                return StatusCode(500);
            }
            else
            {
                bool b = await _lessonService.DeleteLesson(id);
                if (b) return Ok(b);
                else return NotFound(); 
            }
        }


    }
}
