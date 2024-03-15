using API_Dto;
using Asp.Versioning;
using EventLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API_EF.Controllers;

/// <summary>
///  Controller for templates
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly ITemplateService<TemplateDto> _templateService;
    
    private readonly ILogger<TemplatesController> _logger;
    
    public TemplatesController(ITemplateService<TemplateDto> templateService, ILogger<TemplatesController> logger)
    {
        _templateService = templateService;
        _logger = logger;
    }
    
    /// <summary>
    ///  Get all templates
    /// </summary>
    /// <param name="id"></param>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [HttpGet("user/{id}")]
    [ProducesResponseType(200, Type = typeof(PageReponse<TemplateDto>))]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetTemplatesByUserId(long id, int index = 0, int count = 10)
    {
        _logger.LogInformation(LogEvents.GetItems, "GetTemplatesByUserId");
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.GetTemplatesByUserId(id, index, count);
        if (data == null)
        {
            _logger.LogInformation(LogEvents.GetItems, "NoContent");
            return NoContent();
        }
        else
        {
            return Ok(data);
        }
    }
    
    /// <summary>
    ///  Get all empty templates
    /// </summary>
    /// <param name="id"></param>
    /// <param name="index"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    [HttpGet("user/{id}/models")]
    public async Task<IActionResult> GetEmptyTemplatesByUserId(long id, int index = 0, int count = 10)
    {
        _logger.LogInformation(LogEvents.GetItems, "GetEmptyTemplatesByUserId");
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.GetEmptyTemplatesByUserId(id, index, count);
        if (data == null)
        {
            _logger.LogInformation(LogEvents.GetItems, "NoContent");
            return NoContent();
        }
        else
        {
            return Ok(data);
        }
    }
    
    /// <summary>
    ///  Get a template by its id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/user/{userId}")]
    public async Task<IActionResult> GetTemplateById(long userId, long id)
    {
        _logger.LogInformation(LogEvents.GetItem, "GetTemplateById");
        if (_templateService == null)
        {
            return StatusCode(500);
        }
        var data = await _templateService.GetTemplateById(userId, id);
        if (data == null)
        {
            _logger.LogInformation(LogEvents.GetItem, "NoContent");
            return NoContent();
        }
        else
        {
            return Ok(data);
        }
    }
    
    /// <summary>
    ///  Add a template
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="template"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostTemplate(long userId, [FromBody] TemplateDto template)
    {
        _logger.LogInformation(LogEvents.InsertItem, "PostTemplate");
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
    
    /// <summary>
    ///  Update a template
    /// </summary>
    /// <param name="id"></param>
    /// <param name="template"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTemplate(long id, [FromBody] TemplateDto template)
    {
        _logger.LogInformation(LogEvents.UpdateItem, "PutTemplate");
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
    
    /// <summary>
    ///  Delete a template
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTemplate(long id)
    {
        _logger.LogInformation(LogEvents.DeleteItem, "DeleteTemplate");
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