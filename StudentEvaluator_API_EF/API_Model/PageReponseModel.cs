using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Model
{
    public class PageReponseModel<T> where T : class
    {
        public int nbElement;
        public IEnumerable<T> Data { get; set; }

        public PageReponseModel(int nbelement,IEnumerable<T> data) {
            nbElement = nbelement;
            Data = data;
        }
    }
}
