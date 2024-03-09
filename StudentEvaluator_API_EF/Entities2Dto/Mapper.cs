namespace Entities2Dto
{

    /// <summary>
    /// Generic class for mapping between entity and DTO objects.
    /// </summary>
    /// <typeparam name="T">The type of the entity object.</typeparam>
    /// <typeparam name="U">The type of the DTO object.</typeparam>
    public class Mapper<T,U>
    {
        public Dictionary<T, U> map = new Dictionary<T,U>();


        /// <summary>
        /// Gets the DTO object corresponding to the given entity object.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The DTO object corresponding to the entity object, or null if not found.</returns>
        public U? GetDto(T entity)
        {
            if (map.TryGetValue(entity, out U? value)) return value;
            else return default;
         
        }



        /// <summary>
        /// Gets the entity object corresponding to the given DTO object.
        /// </summary>
        /// <param name="dto">The DTO object.</param>
        /// <returns>The entity object corresponding to the DTO object, or null if not found.</returns>
        public T? GetEntity(U dto)
        {
            foreach (KeyValuePair<T, U> pair in map)
            {
                if (pair.Value.Equals(dto)) return pair.Key;
            }
            return default;
        }

        /// <summary>
        /// Sets the mapping between the given entity and DTO objects.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <param name="dto">The DTO object.</param>
        public void Set(T entity, U dto)
        {
            map.Add(entity,dto);
        }


        /// <summary>
        /// Clears the mapping dictionary.
        /// </summary>
        public void Reset()
        {
            map.Clear();
        }
    }
}
