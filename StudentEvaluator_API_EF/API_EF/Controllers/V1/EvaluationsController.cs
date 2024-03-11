using API_Dto;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace API_EF.Controllers.V1
{
    /// <summary>
    ///  Controller for evaluations
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EvaluationsController : ControllerBase
    {

        private readonly IEvaluationService<EvaluationDto,EvaluationReponseDto>  _evaluationService;

        public EvaluationsController(IEvaluationService<EvaluationDto, EvaluationReponseDto> evaluationService)
        {
            _evaluationService = evaluationService;
        }
        
        /// <summary>
        ///  Get all evaluations
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Get an evaluation by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
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

        /// <summary>
        ///  Get all evaluations of a teacher
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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
        
        /// <summary>
        ///  Add an evaluation
        /// </summary>
        /// <param name="eval"></param>
        /// <returns></returns>
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

        /// <summary>
        ///  Update an evaluation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eval"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutEvaluation(long id, [FromBody] EvaluationDto eval)
        {
            if (_evaluationService == null)
            {
                return StatusCode(500);
            }
            var evalDto = await _evaluationService.PutEvaluation(id,eval);
            if (evalDto == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(evalDto);
            }
        }

        /// <summary>
        ///  Delete an evaluation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
