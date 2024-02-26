using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public interface ILessonService
    {
        public Task<PageReponseDto<LessonDto>> GetLessons(int index, int count);
        public Task<LessonDto?> GetLessonById(long id);
        public Task<LessonDto?> PostLesson(LessonDto lesson);
        public Task<LessonDto?> PutLesson(long id, LessonDto lesson);
        public Task<bool> DeleteLesson(long id);
    }
}
