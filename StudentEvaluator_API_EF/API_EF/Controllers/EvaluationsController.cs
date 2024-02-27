using API_Dto;
using Microsoft.AspNetCore.Mvc;

namespace API_EF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluationsController : ControllerBase
    {

        private readonly IEvaluationService  _evaluationService;

        public EvaluationsController(IEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvaluations(int index=0, int count=10)
        {
            if (_evaluationService == null)
            {
                return StatusCode(500);
            }
            var data = await _evaluationService.GetEvaluations(index, count);
            if (data == null) return NoContent();
            else return Ok(data);
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetEvaluationById(long id)
        {
            if (_evaluationService == null)
            {
                return StatusCode(500);
            }
            var eval = await _evaluationService.GetEvaluationById(id);
            if(eval == null)
            {
                return NotFound();
            }
            else return Ok(eval);
        }

        [HttpGet]
        [Route("teacher/{id}")]
        public async Task<IActionResult> GetEvaluationsByTeacherId(long id,int index = 0, int count = 10)
        {
            if (_evaluationService == null)
            {
                return StatusCode(500);
            }
            var data = await _evaluationService.GetEvaluationsByTeacherId(id,index,count);
            if (data == null) return NoContent();
            else return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> PostEvaluation([FromBody] EvaluationDto eval)
        {
            if (_evaluationService == null)
            {
                return StatusCode(500);
            }
            var evalDto = await _evaluationService.PostEvaluation(eval);
            if (evalDto == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(evalDto);
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutEvaluation(long id, [FromBody] EvaluationDto eval)
        {
            if (_evaluationService == null)
            {
                return StatusCode(500);
            }
            var evalDto = await _evaluationService.PostEvaluation(eval);
            if (evalDto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(evalDto);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvaluation(long id)
        {
            if (_evaluationService == null)
            {
                return StatusCode(500);
            }
            else
            {
                bool b = await _evaluationService.DeleteEvaluation(id);
                if (b) return Ok(b);
                else return NotFound(); 
            }
        }


    }
}
