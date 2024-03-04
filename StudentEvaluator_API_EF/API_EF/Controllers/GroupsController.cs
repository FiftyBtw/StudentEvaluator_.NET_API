using API_Dto;
using Microsoft.AspNetCore.Mvc;

namespace API_EF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {

        private readonly IGroupService  _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups(int index=0, int count=10)
        {
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var data = await _groupService.GetGroups(index, count);
            if (data == null) return NoContent();
            else return Ok(data);
        }

        [HttpGet]
        [Route("{gyear}/{gnumber}")]
        public async Task<IActionResult> GetGroupById(int gyear,int gnumber)
        {
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var book = await _groupService.GetGroupByIds(gyear,gnumber);
            if(book == null)
            {
                return NotFound();
            }
            else return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] GroupDto group)
        {
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var studentDto = await _groupService.PostGroup(group);
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
        public async Task<IActionResult> PutGroup(int gyear, int gnumber, [FromBody] GroupDto group)
        {
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var studentDto = await _groupService.PutGroup(gyear,gnumber, group);
            if (studentDto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(studentDto);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(int gyear, int gnumber)
        {
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            else
            {
                bool b = await _groupService.DeleteGroup(gyear,gnumber);
                if (b) return Ok(b);
                else return NotFound(); 
            }
        }


    }
}
