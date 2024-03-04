using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PageReponse<T> where T : class
    {
        public int nbElement { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PageReponse(int nbelement, IEnumerable<T> data) { 
            nbElement= nbelement;
            Data = data;
        }
    }
}
