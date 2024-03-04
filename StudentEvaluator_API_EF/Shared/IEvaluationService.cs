using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;
{
    public interface IEvaluationService<T> where T : class
    {
        public Task<PageReponse<T>> GetEvaluations(int index, int count);
        public Task<T?> GetEvaluationById(long id);
        public Task<PageReponse<T>> GetEvaluationsByTeacherId(long id, int index, int count);
        public Task<T?> PostEvaluation(T eval);
        public Task<T?> PutEvaluation(long id, T eval);
        public Task<bool> DeleteEvaluation(long id);
    }
}
