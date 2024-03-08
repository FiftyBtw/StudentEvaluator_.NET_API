using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Represents a response page containing a collection of data of type T.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the page.</typeparam>
    public class PageReponse<T> where T : class
    {
        public int nbElement { get; set; }
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the PageResponse class.
        /// </summary>
        /// <param name="numberOfElements">The number of elements in the page.</param>
        /// <param name="data">The data contained in the page.</param>
        public PageReponse(int nbelement, IEnumerable<T> data) { 
            nbElement= nbelement;
            Data = data;
        }
    }
}
