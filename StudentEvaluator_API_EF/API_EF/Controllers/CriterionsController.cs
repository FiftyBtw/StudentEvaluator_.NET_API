using API_Dto;
using Microsoft.AspNetCore.Mvc;

namespace API_EF.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CriterionsController : ControllerBase
{
    private readonly ICriteriaService _criteriaService;
    
    public CriterionsController(ICriteriaService criteriaService)
    {
        _criteriaService = criteriaService;
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
    
    [HttpGet("texts/{id}")]
    [ProducesResponseType(200, Type= typeof(TextCriteriaDto))] 
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetTextCriterionById(long id)
    {
        if (_criteriaService == null) {
            return StatusCode(500);
        }
        try {
            var data = await _criteriaService.GetTextCriterionByIds(id);
            if (data == null)
            {
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
    
    [HttpGet("sliders/{id}")]
    [ProducesResponseType(200, Type= typeof(SliderCriteriaDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetSliderCriterionById(long id)
    {
        if (_criteriaService == null) {
            return StatusCode(500);
        }
        try {
            var data = await _criteriaService.GetSliderCriterionByIds(id);
            if (data == null)
            {
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
    
    [HttpGet("radios/{id}")]
    [ProducesResponseType(200, Type= typeof(RadioCriteriaDto))]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public  async Task<IActionResult> GetRadioCriterionById(long id)
    {
        if (_criteriaService == null) {
            return StatusCode(500);
        }
        try {
            var data = await _criteriaService.GetRadioCriterionByIds(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
    
     [HttpGet("template/{id}")]
     [ProducesResponseType(200, Type= typeof(PageReponseDto<CriteriaDto>[]))]
     [ProducesResponseType(400)]
     [ProducesResponseType(404)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> GetCriterionsByTemplateId(long id)
     {
         if (_criteriaService == null) {
             return StatusCode(500);
         }
         try {
             var data = await _criteriaService.GetCriterionsByTemplateId(id);
             if (data == null)
             {
                 return NotFound();
             }
             return Ok(data);
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }
     
     [HttpPost("text/template/{id}")]
     [ProducesResponseType(204)]
     [ProducesResponseType(400)]
     [ProducesResponseType(500)]
     public async Task<IActionResult> PostTextCriterion(long id, [FromBody] TextCriteriaDto text)
     {
         if (_criteriaService == null) {
             return StatusCode(500);
         }
         try {
             var data = await _criteriaService.PostTextCriterion(id, text);
             if (data == null)
             {
                 return BadRequest();
             }
             return Created();
         }
         catch (Exception e) {
             return BadRequest(e.Message);
         }
     }
     
     
         [HttpPost("slider/template/{id}")]
         [ProducesResponseType(204)]
         [ProducesResponseType(400)]
         [ProducesResponseType(500)]
         public async Task<IActionResult> PostSliderCriterion(long id, [FromBody] SliderCriteriaDto slider)
         {
             if (_criteriaService == null) {
                 return StatusCode(500);
             }
             try {
                 var data = await _criteriaService.PostSliderCriterion(id, slider);
                 if (data == null)
                 {
                     return BadRequest();
                 }
                 return Created();
             }
             catch (Exception e) {
                 return BadRequest(e.Message);
             }
         }
     
         [HttpPost("radio/template/{id}")]
         [ProducesResponseType(204)]
         [ProducesResponseType(400)]
         [ProducesResponseType(500)]
         public async Task<IActionResult> PostRadioCriterion(long id, [FromBody] RadioCriteriaDto radio)
         {
             if (_criteriaService == null) {
                 return StatusCode(500);
             }
             try {
                 var data = await _criteriaService.PostRadioCriterion(id, radio);
                 if (data == null)
                 {
                     return BadRequest();
                 }
                 return Created();
             }
             catch (Exception e) {
                 return BadRequest(e.Message);
             }
         }
         
         [HttpPut("text/{id}")]
         [ProducesResponseType(204)]
         [ProducesResponseType(400)]
         [ProducesResponseType(500)]
         public async Task<IActionResult> PutTextCriterion(long id, [FromBody] TextCriteriaDto text)
         {
             if (_criteriaService == null) {
                 return StatusCode(500);
             }
             try {
                 var data = await _criteriaService.PutTextCriterion(id, text);
                 if (data == null)
                 {
                     return BadRequest();
                 }
                 return Created();
             }
             catch (Exception e) {
                 return BadRequest(e.Message);
             }
         }
         
         
         [HttpPut("slider/{id}")]
         [ProducesResponseType(204)]
         [ProducesResponseType(400)]
         [ProducesResponseType(500)]
         public async Task<IActionResult> PutSliderCriterion(long id, [FromBody] SliderCriteriaDto slider)
         {
             if (_criteriaService == null) {
                 return StatusCode(500);
             }
             try {
                 var data = await _criteriaService.PutSliderCriterion(id, slider);
                 if (data == null)
                 {
                     return BadRequest();
                 }
                 return Created();
             }
             catch (Exception e) {
                 return BadRequest(e.Message);
             }
         }
     
         [HttpPut("radio/{id}")]
         [ProducesResponseType(204)]
         [ProducesResponseType(400)]
         [ProducesResponseType(500)]
         public async Task<IActionResult> PutRadioCriterion(long id, [FromBody] RadioCriteriaDto radio)
         {
             if (_criteriaService == null) {
                 return StatusCode(500);
             }
             try {
                 var data = await _criteriaService.PutRadioCriterion(id, radio);
                 if (data == null)
                 {
                     return BadRequest();
                 }
                 return Created();
             }
             catch (Exception e) {
                 return BadRequest(e.Message);
             }
         }

         [HttpDelete("{id}")]
         [ProducesResponseType(204)]
         [ProducesResponseType(400)]
         [ProducesResponseType(500)]
         public async Task<IActionResult> DeleteCriterion(long id)
         {
             if (_criteriaService == null) {
                 return StatusCode(500);
             }
             try {
                 var data = await _criteriaService.DeleteCriteria(id);
                 if (data == null)
                 {
                     return BadRequest();
                 }
                 return Created();
             }
             catch (Exception e) {
                 return BadRequest(e.Message);
             }
         }
}