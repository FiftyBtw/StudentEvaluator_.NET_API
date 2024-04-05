using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

    /// <summary>
    /// Represents a service interface for lesson-related operations.
    /// </summary>
    /// <typeparam name="T">The type representing a lesson.</typeparam>
    /// <typeparam name="TResponse">The type representing a response related to the lesson.</typeparam>
    public interface ILessonService<T,TResponse> where T : class where TResponse : class
    {
        public Task<PageReponse<TResponse>> GetLessons(int index = 0, int count = 10);
        public Task<TResponse?> GetLessonById(long id);
        public Task<PageReponse<TResponse>> GetLessonsByTeacherId(string userId, int index = 0,int count = 10);
        public Task<TResponse?> PostLesson(T lesson);
        public Task<TResponse?> PutLesson(long id, T lesson);
        public Task<bool> DeleteLesson(long id);
    }

