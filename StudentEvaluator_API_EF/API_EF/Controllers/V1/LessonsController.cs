using System.Security.Claims;
using API_Dto;
using Asp.Versioning;
using EventLogs;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class LessonsController : ControllerBase
    {

        private readonly ILessonService<LessonDto,LessonReponseDto>  _lessonService;
        
        private readonly ILogger<LessonsController> _logger;

        public LessonsController(ILessonService<LessonDto, LessonReponseDto> lessonService, ILogger<LessonsController> logger)
        {
            _lessonService = lessonService;
            _logger = logger;
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
            _logger.LogInformation(LogEvents.GetItems, "GetLessons");
            if (_lessonService == null)return StatusCode(500);

            var data = await _lessonService.GetLessons(index, count);
            if (data == null)
            {
                _logger.LogInformation(LogEvents.GetItemsNoContent, "GetLessons");
                return NoContent();
            }
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
            _logger.LogInformation(LogEvents.GetItem, "GetLessonById");
            if (_lessonService == null) return StatusCode(500);

            var lesson = await _lessonService.GetLessonById(id);
            if(lesson == null)
            {
                _logger.LogInformation(LogEvents.GetItemNotFound, "GetLessonById");
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
        [Route("teacher")]
        public async Task<IActionResult> GetLessonsByTeacher(int index = 0, int count = 10)
        {
            _logger.LogInformation(LogEvents.GetItems, "GetLessonsByTeacherId");
            if (_lessonService == null) return StatusCode(500);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID is missing in the token");
            }

            var data = await _lessonService.GetLessonsByTeacherId(userId, index, count);
            if (data == null)
            {
                _logger.LogInformation(LogEvents.GetItemsNoContent, "GetLessonsByTeacherId");
                return NoContent();
            }

            return Ok(data);
        }


        /// <summary>
        ///  Add a lesson
        /// </summary>
        /// <param name="lesson"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostLesson([FromBody] LessonDto lesson)
        {
            _logger.LogInformation(LogEvents.InsertItem, "PostLesson");
            if (_lessonService == null) return StatusCode(500);

            var lessonDto = await _lessonService.PostLesson(lesson);
            if (lessonDto == null)
            {
                _logger.LogInformation(LogEvents.InsertItemBadRequest, "PostLesson");
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
            _logger.LogInformation(LogEvents.UpdateItem, "PutLesson");
            if (_lessonService == null)return StatusCode(500);

            var lessonDto = await _lessonService.PutLesson(id,lesson);
            if (lessonDto == null)
            {
                _logger.LogInformation(LogEvents.UpdateItemBadRequest, "PutLesson");
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
            _logger.LogInformation(LogEvents.DeleteItem, "DeleteLesson");
            if (_lessonService == null)return StatusCode(500);

            else
            {
                bool b = await _lessonService.DeleteLesson(id);
                if (b) return Ok(b);
                else
                {
                    _logger.LogInformation(LogEvents.DeleteItemNotFound, "DeleteLesson");
                    return NotFound();
                }
            }
        }
    }
}
