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
    
    [HttpGet]
    public async Task<IActionResult> GetTemplatesByUserId(long userId, int index = 1, int count = 10)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.GetTemplatesByUserId(userId, index, count);
        if (data == null)
        {
            return NoContent();
        }
        else
        {
            return Ok(data);
        }
    }
    
    [HttpGet("empty")]
    public async Task<IActionResult> GetEmptyTemplatesByUserId(long userId, int index = 1, int count = 10)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.GetEmptyTemplatesByUserId(userId, index, count);
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
    public async Task<IActionResult> GetTemplateById(long userId, long templateId)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.GetTemplateById(userId, templateId);
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
    public async Task<IActionResult> PutTemplate(long userId, long templateId, [FromBody] TemplateDto template)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.PutTemplate(userId, templateId, template);
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
    public async Task<IActionResult> DeleteTemplate(long userId, long templateId)
    {
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.DeleteTemplate(userId, templateId);
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