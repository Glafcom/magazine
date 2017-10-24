using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.DALExtension.Interfaces {
    public interface IGenericRepository<TEntity> where TEntity : class {
        /// <summary>
        /// Getting a list of entity instances, filtered and sorted according to inputs
        /// </summary>
        /// <param name="filter">Filter statement as a LINQ query</param>
        /// <param name="orderBy">Statement as a LINQ query, that defines order</param>
        /// <param name="includeProperties">List of properties that should be included into result</param>
        /// <returns>List of entities that satisfies all conditions</returns>
        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        /// <summary>
        /// Receiving entity instance by its ID
        /// </summary>
        /// <param name="id">Id of target entity instanse</param>
        /// <returns>Selected Instance of the entity</returns>
        TEntity GetByID(object id);

        /// <summary>
        /// Adding entity instance in the database
        /// </summary>
        /// <param name="entity">An instance of the entity to be added</param>
        TEntity Insert(TEntity entity);
    }
}
