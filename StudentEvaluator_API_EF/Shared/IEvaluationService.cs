using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Shared;


    /// <summary>
    /// Represents a service interface for evaluation-related operations.
    /// </summary>
    /// <typeparam name="T">The type representing an evaluation.</typeparam>
    /// <typeparam name="TResponse">The type representing a response related to the evaluation.</typeparam>
    public interface IEvaluationService<T,TResponse> where T : class where TResponse : class
    {
        public Task<PageReponse<TResponse>> GetEvaluations(int index = 0, int count = 10);
        public Task<TResponse?> GetEvaluationById(long id);
        public Task<PageReponse<TResponse>> GetEvaluationsByTeacherId(string userId, int index = 0, int count = 10);
        public Task<TResponse?> PostEvaluation(T eval);
        public Task<TResponse?> PutEvaluation(long id, T eval);
        public Task<bool> DeleteEvaluation(long id);
    }

