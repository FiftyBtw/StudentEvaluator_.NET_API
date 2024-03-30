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
    public interface ILessonService<T,TReponse> where T : class where TReponse : class
    {
        public Task<PageReponse<TReponse>> GetLessons(int index, int count);
        public Task<TReponse?> GetLessonById(long id);
        public Task<PageReponse<TReponse>> GetLessonsByTeacherId(string userId, int index,int count);
        public Task<TReponse?> PostLesson(T lesson);
        public Task<TReponse?> PutLesson(long id, T lesson);
        public Task<bool> DeleteLesson(long id);
    }

