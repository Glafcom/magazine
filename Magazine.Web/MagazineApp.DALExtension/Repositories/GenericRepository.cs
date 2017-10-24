using MagazineApp.DALExtension.AppDbContext;
using MagazineApp.DALExtension.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MagazineApp.DAL.Repositories {
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class {
        /// <summary>
        /// DBContext to interact with the database
        /// </summary>        
        private AuditDbContext _context;

        /// <summary>
        /// DBSet to interact with the table "Orders"
        /// </summary>
        private DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor of the order's repository
        /// </summary>
        /// <param name="context">DBContext to interact with the database</param>
        public GenericRepository(AuditDbContext context) {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Getting a list of orders, filtered and sorted according to inputs
        /// </summary>
        /// <param name="filter">Filter statement as a LINQ query</param>
        /// <param name="orderBy">Statement as a LINQ query, that defines order</param>
        /// <param name="includeProperties">List of properties that should be included into result</param>
        /// <returns>List of entities that satisfies all conditions</returns>
        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "") {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null) {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) {
                return orderBy(query);
            }
            else {
                return query;
            }
        }

        /// <summary>
        /// Receiving order instance by its ID
        /// </summary>
        /// <param name="id">Id of target order instance</param>
        /// <returns>Selected order</returns>
        public virtual TEntity GetByID(object id) {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Inserting order in the database
        /// </summary>
        /// <param name="entity">An order to be inserted</param>
        public virtual TEntity Insert(TEntity entity) {
            var newEntity = _dbSet.Add(entity);
            _context.SaveChanges();
            return newEntity;
        }
    }
}
