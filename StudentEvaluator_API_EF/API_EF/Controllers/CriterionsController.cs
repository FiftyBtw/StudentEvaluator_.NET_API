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
    
    [HttpGet("texts")]
    public async Task<IActionResult> GetTextCriterions(int? index, int? count)
    {
        if (_criteriaService == null) {
            return StatusCode(500);
        }
        try {
            var data = await _criteriaService.GetTextCriterions(index ?? 0, count ?? 10);
            return Ok(data);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("texts/{id}")]
    public async Task<IActionResult> GetTextCriterionById(long id)
    {
        if (_criteriaService == null) {
            return StatusCode(500);
        }
        try {
            var data = await _criteriaService.GetTextCriterionByIds(id);
            return Ok(data);
        }
        catch (Exception e) {
            return BadRequest(e.Message);
        }
    }
    
}