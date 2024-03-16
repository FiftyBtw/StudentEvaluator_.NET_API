using API_Dto;
using Asp.Versioning;
using EventLogs;
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
        
        private readonly ILogger<EvaluationsController> _logger;

        public EvaluationsController(IEvaluationService<EvaluationDto, EvaluationReponseDto> evaluationService, ILogger<EvaluationsController> logger)
        {
            _evaluationService = evaluationService;
            _logger = logger;
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
            _logger.LogInformation(LogEvents.GetItems, "GetEvaluations");
            if (_evaluationService == null) return StatusCode(500);
            var data = await _evaluationService.GetEvaluations(index, count);
            if (data == null)
            {
                _logger.LogInformation(LogEvents.GetItemsNoContent, "GetEvaluations");
                return NoContent();
            }
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
            _logger.LogInformation(LogEvents.GetItem, "GetEvaluationById");
            if (_evaluationService == null)return StatusCode(500);

            var eval = await _evaluationService.GetEvaluationById(id);
            if(eval == null)
            {
                _logger.LogInformation(LogEvents.GetItemNotFound, "GetEvaluationById");
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
            _logger.LogInformation(LogEvents.GetItems, "GetEvaluationsByTeacherId");
            if (_evaluationService == null)return StatusCode(500);

            var data = await _evaluationService.GetEvaluationsByTeacherId(id,index,count);
            if (data == null)
            {
                _logger.LogInformation(LogEvents.GetItemsNotFound, "GetEvaluationsByTeacherId");
                return NoContent();
            }
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
            _logger.LogInformation(LogEvents.InsertItem, "PostEvaluation");
            if (_evaluationService == null) return StatusCode(500);

            var evalDto = await _evaluationService.PostEvaluation(eval);
            if (evalDto == null)
            {
                _logger.LogInformation(LogEvents.InsertItemBadRequest, "PostEvaluation");
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
            _logger.LogInformation(LogEvents.UpdateItem, "PutEvaluation");
            if (_evaluationService == null) return StatusCode(500);

            var evalDto = await _evaluationService.PutEvaluation(id,eval);
            if (evalDto == null)
            {
                _logger.LogInformation(LogEvents.UpdateItemBadRequest, "PutEvaluation");
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
            _logger.LogInformation(LogEvents.DeleteItem, "DeleteEvaluation");
            if (_evaluationService == null)return StatusCode(500);

            else
            {
                bool b = await _evaluationService.DeleteEvaluation(id);
                if (b) return Ok(b);
                else
                {
                    _logger.LogInformation(LogEvents.DeleteItemBadRequest, "DeleteEvaluation");
                    return NotFound();
                }
            }
        }
    }
}
