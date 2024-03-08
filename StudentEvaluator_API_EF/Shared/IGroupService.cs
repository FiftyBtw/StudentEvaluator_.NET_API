using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared;

    public interface IGroupService<T> where T : class
    {
        public Task<PageReponse<T>> GetGroups(int index,int count);

        public Task<T?> GetGroupByIds(int gyear, int gnumber);
        public Task<T?> PostGroup(T group);

        public Task<T?> PutGroup(int gyear, int gnumber, T group);

        public Task<bool> DeleteGroup(int gyear, int gnumber);
    }

