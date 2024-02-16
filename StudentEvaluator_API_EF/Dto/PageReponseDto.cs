using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Dto
{
    public class PageReponseDto<T> where T : class
    {
        public int nbElement { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PageReponseDto(int nbE, IEnumerable<T> data) { 
            nbElement= nbE;
            Data = data;
        }
    }
}
