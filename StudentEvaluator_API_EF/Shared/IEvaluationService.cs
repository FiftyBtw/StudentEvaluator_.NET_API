using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

    public interface IEvaluationService<T,TReponse> where T : class where TReponse : class
    {
        public Task<PageReponse<TReponse>> GetEvaluations(int index, int count);
        public Task<TReponse?> GetEvaluationById(long id);
        public Task<PageReponse<TReponse>> GetEvaluationsByTeacherId(long id, int index, int count);
        public Task<TReponse?> PostEvaluation(T eval);
        public Task<TReponse?> PutEvaluation(long id, T eval);
        public Task<bool> DeleteEvaluation(long id);
    }

