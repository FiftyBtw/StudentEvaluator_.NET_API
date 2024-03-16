using API_Dto;
using Asp.Versioning;
using EventLogs;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API_EF.Controllers.V1;

/// <summary>
///  This class is the controller for the criterions
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CriterionsController : ControllerBase
{
    private readonly ICriteriaService<CriteriaDto,TextCriteriaDto,SliderCriteriaDto,RadioCriteriaDto> _criteriaService;
    private readonly ILogger<CriterionsController> _logger;
    
    public CriterionsController(ICriteriaService<CriteriaDto, TextCriteriaDto, SliderCriteriaDto, RadioCriteriaDto> criteriaService, ILogger<CriterionsController> logger)
    {
        _criteriaService = criteriaService;
        _logger = logger;
    }
    
    /*
    [HttpGet("texts")]
    [ProducesResponseType(200, Type= typeof(PageReponseDto<TextCriteriaDto>[]))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetTextCriterions(int index = 0, int count = 10)
    {
        if (_criteriaService == null) {
            return StatusCode(500);
        }
        try {
            var data = await _criteriaService.GetTextCriterions(index , count);
            if (data.nbElement == 0)
            {
                return NoContent();
            }
            return Ok(data);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    */
    
    /// <summary>
    ///  Get a text criteria by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("texts/{id}")]
    [ProducesResponseType(200, Type= typeof(TextCriteriaDto))] 
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetTextCriterionById(long id)
    {
        _logger.LogInformation(LogEvents.GetItem, "Getting item {Id}", id);
        if (_criteriaService == null)return StatusCode(500);

        try {
            var data = await _criteriaService.GetTextCriterionByIds(id);
            if (data == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Get({Id}) NOT FOUND", id);
                return NotFound();
            }
            return Ok(data);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
    /*
    [HttpGet("sliders")]
    [ProducesResponseType(200, Type= typeof(PageReponseDto<SliderCriteriaDto>[]))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetSliderCriterions(int index = 0, int count = 10)
    {
        if (_criteriaService == null) {
            return StatusCode(500);
        }
        try {
            var data = await _criteriaService.GetSliderCriterions(index, count);
            if (data.nbElement == 0)
            {
                return NoContent();
            }
            return Ok(data);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    */
    
    /// <summary>
    ///  Get a slider criteria by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("sliders/{id}")]
    [ProducesResponseType(200, Type= typeof(SliderCriteriaDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetSliderCriterionById(long id)
    {
        _logger.LogInformation(LogEvents.GetItem, "Getting item {Id}", id);
        if (_criteriaService == null)return StatusCode(500);

        try {
            var data = await _criteriaService.GetSliderCriterionByIds(id);
            if (data == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Get({Id}) NOT FOUND", id);
                return NotFound();
            }
            return Ok(data);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
    /*
    [HttpGet("radios")]
    [ProducesResponseType(200, Type= typeof(PageReponseDto<RadioCriteriaDto>[]))]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetRadioCriterions(int index = 0, int count = 10)
    {
        if (_criteriaService == null) {
            return StatusCode(500);
        }
        try {
            var data = await _criteriaService.GetRadioCriterions(index, count);
            if (data.nbElement == 0)
            {
                return NoContent();
            }
            return Ok(data);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    */
    
    /// <summary>
    ///  Get a radio criteria by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("radios/{id}")]
    [ProducesResponseType(200, Type= typeof(RadioCriteriaDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public  async Task<IActionResult> GetRadioCriterionById(long id)
    {
        _logger.LogInformation(LogEvents.GetItem, "Getting item {Id}", id);
        if (_criteriaService == null) {
            return StatusCode(500);
        }
        try {
            var data = await _criteriaService.GetRadioCriterionByIds(id);
            if (data == null)
            {
                _logger.LogWarning(LogEvents.GetItemNotFound, "Get({Id}) NOT FOUND", id);
                return NotFound();
            }
            return Ok(data);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
    /// <summary>
    ///  Get all criterions by template id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
     [HttpGet("template/{id}")]
     [ProducesResponseType(200, Type= typeof(PageReponse<CriteriaDto>[]))]
     [ProducesResponseType(400)]
     [ProducesResponseType(404)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> GetCriterionsByTemplateId(long id)
     {
         _logger.LogInformation(LogEvents.GetItem, "Getting item {Id}", id);
         if (_criteriaService == null) {
             return StatusCode(500);
         }
         try {
             var data = await _criteriaService.GetCriterionsByTemplateId(id);
             if (data == null)
             {
                    _logger.LogWarning(LogEvents.GetItemNotFound, "Get({Id}) NOT FOUND", id);
                 return NotFound();
             }
             return Ok(data);
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }
     
    /// <summary>
    ///  Add a text criterion to a template
    /// </summary>
    /// <param name="id"></param>
    /// <param name="text"></param>
    /// <returns></returns>
     [HttpPost("text/template/{id}")]
     [ProducesResponseType(204)]
     [ProducesResponseType(400)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> PostTextCriterion(long id, [FromBody] TextCriteriaDto text)
     {
         _logger.LogInformation(LogEvents.InsertItem, "Inserting item {Id}", id);
         if (_criteriaService == null)return StatusCode(500);

         try {
             var data = await _criteriaService.PostTextCriterion(id, text);
             if (data == null)
             {
                 return BadRequest();
             }
             return Ok(data);
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }
     
     /// <summary>
     ///  Add a slider criterion to a template
     /// </summary>
     /// <param name="id"></param>
     /// <param name="slider"></param>
     /// <returns></returns>
     [HttpPost("slider/template/{id}")]
     [ProducesResponseType(204)]
     [ProducesResponseType(400)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> PostSliderCriterion(long id, [FromBody] SliderCriteriaDto slider)
     {
         _logger.LogInformation(LogEvents.InsertItem, "Inserting item {Id}", id);
         if (_criteriaService == null)return StatusCode(500);

         try {
             var data = await _criteriaService.PostSliderCriterion(id, slider);
             if (data == null)
             {
                 _logger.LogWarning(LogEvents.InsertItemBadRequest, "Insert({Id}) BAD REQUEST", id);
                 return BadRequest();
             }
             return Ok(data);
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }
 
     /// <summary>
     ///  Add a radio criterion to a template
     /// </summary>
     /// <param name="id"></param>
     /// <param name="radio"></param>
     /// <returns></returns>
     [HttpPost("radio/template/{id}")]
     [ProducesResponseType(204)]
     [ProducesResponseType(400)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> PostRadioCriterion(long id, [FromBody] RadioCriteriaDto radio)
     {
            _logger.LogInformation(LogEvents.InsertItem, "Inserting item {Id}", id);
         if (_criteriaService == null)return StatusCode(500);
         try {
             var data = await _criteriaService.PostRadioCriterion(id, radio);
             if (data == null)
             {
                 _logger.LogWarning(LogEvents.InsertItemBadRequest, "Insert({Id}) BAD REQUEST", id);
                 return BadRequest();
             }
             return Ok(data);
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }
     
     /// <summary>
     ///  Update a text criteria
     /// </summary>
     /// <param name="id"></param>
     /// <param name="text"></param>
     /// <returns></returns>
     [HttpPut("text/{id}")]
     [ProducesResponseType(204)]
     [ProducesResponseType(400)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> PutTextCriterion(long id, [FromBody] TextCriteriaDto text)
     {
            _logger.LogInformation(LogEvents.UpdateItem, "Updating item {Id}", id);
         if (_criteriaService == null)return StatusCode(500);

         try {
             var data = await _criteriaService.PutTextCriterion(id, text);
             if (data == null)
             {
                 _logger.LogWarning(LogEvents.UpdateItemBadRequest, "Update({Id}) BAD REQUEST", id);
                 return NotFound();
             }
             return Ok(data);
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }
     
     /// <summary>
     ///  Update a slider criteria
     /// </summary>
     /// <param name="id"></param>
     /// <param name="slider"></param>
     /// <returns></returns>
     [HttpPut("slider/{id}")]
     [ProducesResponseType(204)]
     [ProducesResponseType(400)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> PutSliderCriterion(long id, [FromBody] SliderCriteriaDto slider)
     {
            _logger.LogInformation(LogEvents.UpdateItem, "Updating item {Id}", id);
         if (_criteriaService == null)return StatusCode(500);

         try {
             var data = await _criteriaService.PutSliderCriterion(id, slider);
             if (data == null)
             {
                    _logger.LogWarning(LogEvents.UpdateItemBadRequest, "Update({Id}) BAD REQUEST", id);
                 return NotFound();
             }
             return Ok(data);
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }
 
     /// <summary>
     ///  Update a radio criteria
     /// </summary>
     /// <param name="id"></param>
     /// <param name="radio"></param>
     /// <returns></returns>
     [HttpPut("radio/{id}")]
     [ProducesResponseType(204)]
     [ProducesResponseType(400)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> PutRadioCriterion(long id, [FromBody] RadioCriteriaDto radio)
     {
         _logger.LogInformation(LogEvents.UpdateItem, "Updating item {Id}", id);
         if (_criteriaService == null)return StatusCode(500);

         try {
             var data = await _criteriaService.PutRadioCriterion(id, radio);
             if (data == null)
             {
                 _logger.LogWarning(LogEvents.UpdateItemBadRequest, "Update({Id}) BAD REQUEST", id);
                 return NotFound();
             }
             return Ok(data);
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }

     /// <summary>
     ///  Delete a criteria
     /// </summary>
     /// <param name="id"></param>
     /// <returns></returns>
     [HttpDelete("{id}")]
     [ProducesResponseType(204)]
     [ProducesResponseType(400)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> DeleteCriterion(long id)
     {
         _logger.LogInformation(LogEvents.DeleteItem, "Deleting item {Id}", id);
         if (_criteriaService == null)return StatusCode(500);

         try {
             var data = await _criteriaService.DeleteCriteria(id);
             if (data == false)
             {
                 _logger.LogWarning(LogEvents.DeleteItemBadRequest, "Delete({Id}) BAD REQUEST", id);
                 return NotFound();
             }
             return Ok(data);
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }
}   