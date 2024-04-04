namespace Shared
{
    /// <summary>
    /// Represents a response page containing a collection of data of type T.
    /// </summary>
    /// <typeparam name="T">The type of data contained in the page.</typeparam>
    public class PageReponse<T> where T : class
    {
        public int NbElement { get; set; }
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the PageResponse class.
        /// </summary>
        /// <param name="nbElement">The number of elements in the page.</param>
        /// <param name="data">The data contained in the page.</param>
        public PageReponse(int nbElement, IEnumerable<T> data) { 
            NbElement= nbElement;
            Data = data;
        }
    }
}
