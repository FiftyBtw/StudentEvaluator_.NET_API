using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

    /// <summary>
    /// Represents a service interface for group-related operations.
    /// </summary>
    /// <typeparam name="T">The type representing a group.</typeparam>
    public interface IGroupService<T> where T : class
    {
        public Task<PageReponse<T>> GetGroups(int index = 0, int count = 10);

        public Task<T?> GetGroupByIds(int gyear, int gnumber);
        public Task<T?> PostGroup(T group);
        
        public Task<bool> DeleteGroup(int gyear, int gnumber);
    }

