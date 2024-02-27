using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public interface IEvaluationService
    {
        public Task<PageReponseDto<EvaluationDto>> GetEvaluations(int index, int count);
        public Task<EvaluationDto?> GetEvaluationById(long id);
        public Task<PageReponseDto<EvaluationDto>> GetEvaluationsByTeacherId(long id, int index, int count);
        public Task<EvaluationDto?> PostEvaluation(EvaluationDto eval);
        public Task<EvaluationDto?> PutEvaluation(long id, EvaluationDto eval);
        public Task<bool> DeleteEvaluation(long id);
    }
}
