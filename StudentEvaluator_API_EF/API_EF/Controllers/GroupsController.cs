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
        public async Task<IActionResult> GetGroups(int index, int count)
        {
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var data = await _groupService.GetGroups(index, count);
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
        public async Task<IActionResult> GetGroupById(long id)
        {
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var book = await _groupService.GetGroupByIds(id);
            if(book == null)
            {
                return StatusCode(201);
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
        public async Task<IActionResult> PutGroup(long id, [FromBody] GroupDto group)
        {
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var studentDto = await _groupService.Putgroup(id, group);
            if (studentDto == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(studentDto);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudent(long id)
        {
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            else
            {
                bool b = await _groupService.DeleteGroup(id);
                if (b)
                {
                    return Ok(b);
                }
                else
                { return BadRequest(); }
            }
        }


    }
}
