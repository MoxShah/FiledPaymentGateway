namespace Filed.PaymentGateway.DataInterface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> Get(int id);
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task Add(T entity);
        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);
        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);
        /// <summary>
        /// Gets all include all.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllIncludeAll();
    }
}
