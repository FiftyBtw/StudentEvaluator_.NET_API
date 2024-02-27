using API_Dto;
using Microsoft.AspNetCore.Mvc;

namespace API_EF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {

        private readonly ILessonService  _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

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
