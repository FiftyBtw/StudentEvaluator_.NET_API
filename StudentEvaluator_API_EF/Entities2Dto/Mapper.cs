using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities2Dto
{
    public class Mapper<T,U>
    {
        public Dictionary<T, U> map = new Dictionary<T,U>();

        public U? GetDto(T entity)
        {
            if (map.ContainsKey(entity)) return map[entity];
            else return default;
         
        }

        public T? GetEntity(U dto)
        {
            foreach (KeyValuePair<T, U> pair in map)
            {
                if (pair.Value.Equals(dto)) return pair.Key;
            }
            return default;
        }
        public void Set(T entity, U dto)
        {
            map.Add(entity,dto);
        }


    }
}
