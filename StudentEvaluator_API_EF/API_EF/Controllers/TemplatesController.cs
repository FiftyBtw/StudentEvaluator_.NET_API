using API_Dto;
using Microsoft.AspNetCore.Mvc;

namespace API_EF.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly ITemplateService _templateService;
    
    public TemplatesController(ITemplateService templateService)
    {
        _templateService = templateService;
    }
    
    [HttpGet("user/{id}")]
    [ProducesResponseType(200, Type = typeof(PageReponseDto<TemplateDto>))]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetTemplatesByUserId(long id, int index = 0, int count = 10)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.GetTemplatesByUserId(id, index, count);
        if (data == null)
        {
            return NoContent();
        }
        else
        {
            return Ok(data);
        }
    }
    
    [HttpGet("user/{id}/models")]
    public async Task<IActionResult> GetEmptyTemplatesByUserId(long id, int index = 0, int count = 10)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.GetEmptyTemplatesByUserId(id, index, count);
        if (data == null)
        {
            return NoContent();
        }
        else
        {
            return Ok(data);
        }
    }
    
    [HttpGet("{id}/user/{userId}")]
    public async Task<IActionResult> GetTemplateById(long userId, long id)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.GetTemplateById(userId, id);
        if (data == null)
        {
            return NoContent();
        }
        else
        {
            return Ok(data);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> PostTemplate(long userId, [FromBody] TemplateDto template)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.PostTemplate(userId, template);
        if (data == null)
        {
            return StatusCode(500);
        }
        else
        {
            return Ok(data);
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTemplate(long id, [FromBody] TemplateDto template)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.PutTemplate(id, template);
        if (data == null)
        {
            return StatusCode(500);
        }
        else
        {
            return Ok(data);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTemplate(long id)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.DeleteTemplate(id);
        if (data == false)
        {
            return StatusCode(500);
        }
        else
        {
            return Ok();
        }
    }
    
}