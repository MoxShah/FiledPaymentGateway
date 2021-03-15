namespace Filed.PaymentGateway.DataImplementation
{
    using Filed.PaymentGateway.Data;
    using Filed.PaymentGateway.DataInterface;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Generic repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Filed.PaymentGateway.DataInterface.IGenericRepository{T}" />
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        /// <summary>
        /// The d b context
        /// </summary>
        protected readonly FiledDBContext dBContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{T}"/> class.
        /// </summary>
        /// <param name="dBContext">The d b context.</param>
        protected GenericRepository(FiledDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<T> Get(int id)
        {
            return await dBContext.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAll()
        {
            return await dBContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task Add(T entity)
        {
            await dBContext.Set<T>().AddAsync(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(T entity)
        {
            dBContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(T entity)
        {
            dBContext.Set<T>().Update(entity);
        }

        /// <summary>
        /// Gets all include all.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetAllIncludeAll()
        {
            var includePaths = GetIncludePaths(dBContext);
            var querySet = dBContext.Set<T>().AsQueryable();
            foreach (var includePath in includePaths)
            {
                querySet = querySet.Include(includePath);
            }

            return await querySet.ToListAsync();
        }

        /// <summary>
        /// Gets the include paths.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="maxDepth">The maximum depth.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">maxDepth</exception>
        private static IEnumerable<string> GetIncludePaths(DbContext context, int maxDepth = int.MaxValue)
        {
            if (maxDepth < 0)
                throw new ArgumentOutOfRangeException(nameof(maxDepth));

            var entityType = context.Model.FindEntityType(typeof(T));
            var includedNavigations = new HashSet<INavigation>();
            var stack = new Stack<IEnumerator<INavigation>>();

            while (true)
            {
                var entityNavigations = new List<INavigation>();

                if (stack.Count <= maxDepth)
                {
                    foreach (var navigation in entityType.GetNavigations())
                    {
                        if (includedNavigations.Add(navigation))
                            entityNavigations.Add(navigation);
                    }
                }

                if (entityNavigations.Count == 0)
                {
                    if (stack.Count > 0)
                        yield return string.Join(".", stack.Reverse().Select(e => e.Current!.Name));
                }
                else
                {
                    foreach (var navigation in entityNavigations)
                    {
                        var inverseNavigation = navigation.Inverse;
                        if (inverseNavigation != null)
                            includedNavigations.Add(inverseNavigation);
                    }

                    stack.Push(entityNavigations.GetEnumerator());
                }

                while (stack.Count > 0 && !stack.Peek().MoveNext())
                    stack.Pop();

                if (stack.Count == 0)
                    break;

                entityType = stack.Peek().Current!.TargetEntityType;
            }
        }
    }
}
