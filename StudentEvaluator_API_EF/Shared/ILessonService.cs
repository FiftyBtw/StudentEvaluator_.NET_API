using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

    public interface ILessonService<T,TReponse> where T : class where TReponse : class
    {
        public Task<PageReponse<TReponse>> GetLessons(int index, int count);
        public Task<TReponse?> GetLessonById(long id);
        public Task<PageReponse<TReponse>> GetLessonsByTeacherId(long id, int index,int count);
        public Task<TReponse?> PostLesson(T lesson);
        public Task<TReponse?> PutLesson(long id, T lesson);
        public Task<bool> DeleteLesson(long id);
    }

