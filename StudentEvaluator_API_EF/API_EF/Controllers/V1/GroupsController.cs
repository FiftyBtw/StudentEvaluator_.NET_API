using API_Dto;
using Asp.Versioning;
using EventLogs;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API_EF.Controllers.V1
{
    /// <summary>
    ///  Controller for groups
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GroupsController : ControllerBase
    {

        private readonly IGroupService<GroupDto>  _groupService;
        
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IGroupService<GroupDto> groupService, ILogger<GroupsController> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }

        /// <summary>
        ///  Get all groups
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetGroups(int index=0, int count=10)
        {
            _logger.LogInformation(LogEvents.GetItems, "GetGroups");
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var data = await _groupService.GetGroups(index, count);
            if (data == null)
            {
                _logger.LogInformation(LogEvents.GetItemsNoContent, "GetGroups");
                return NoContent();
            }
            else return Ok(data);
        }
    
        /// <summary>
        ///  Get a group by the year and the number 
        /// </summary>
        /// <param name="gyear"></param>
        /// <param name="gnumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{gyear}/{gnumber}")]
        public async Task<IActionResult> GetGroupById(int gyear,int gnumber)
        {
            _logger.LogInformation(LogEvents.GetItem, "GetGroupById");
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var book = await _groupService.GetGroupByIds(gyear,gnumber);
            if(book == null)
            {
                _logger.LogInformation(LogEvents.GetItemNotFound, "GetGroupById");
                return NotFound();
            }
            else return Ok(book);
        }

        /// <summary>
        ///  Add a group
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostGroup([FromBody] GroupDto group)
        {
            _logger.LogInformation(LogEvents.InsertItem, "PostGroup");
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            var studentDto = await _groupService.PostGroup(group);
            if (studentDto == null)
            {
                _logger.LogInformation(LogEvents.InsertItemBadRequest, "PostGroup");
                return BadRequest();
            }
            else
            {
                return Ok(studentDto);
            }
        }

        /// <summary>
        ///  Delete a group
        /// </summary>
        /// <param name="gyear"></param>
        /// <param name="gnumber"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteGroup(int gyear, int gnumber)
        {
            _logger.LogInformation(LogEvents.DeleteItem, "DeleteGroup");
            if (_groupService == null)
            {
                return StatusCode(500);
            }
            else
            {
                bool b = await _groupService.DeleteGroup(gyear,gnumber);
                if (b) return Ok(b);
                else
                {
                    _logger.LogInformation(LogEvents.DeleteItemNotFound, "DeleteGroup");
                    return NotFound();
                }
            }
        }
    }
}
