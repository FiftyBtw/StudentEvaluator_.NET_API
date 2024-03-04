using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;
{
    public interface ILessonService<T> where T : class
    {
        public Task<PageReponse<T>> GetLessons(int index, int count);
        public Task<T?> GetLessonById(long id);
        public Task<PageReponse<T>> GetLessonsByTeacherId(long id, int index,int count);
        public Task<T?> PostLesson(T lesson);
        public Task<T?> PutLesson(long id, T lesson);
        public Task<bool> DeleteLesson(long id);
    }
}
