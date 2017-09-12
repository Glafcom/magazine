using MagazineApp.Contracts.DALContracts;
using MagazineApp.DAL.AppDbContext;
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
        private MagazineAppDbContext _context;

        /// <summary>
        /// DBSet to interact with the table "Orders"
        /// </summary>
        private DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructor of the order's repository
        /// </summary>
        /// <param name="context">DBContext to interact with the database</param>
        public GenericRepository(MagazineAppDbContext context) {
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

        /// <summary>
        /// Deleting order from the database
        /// </summary>
        /// <param name="id">Id of target order</param>
        public virtual void Delete(object id) {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deleting order from the database
        /// </summary>
        /// <param name="entityToDelete">Target order</param>
        public virtual void Delete(TEntity entityToDelete) {
            if (_context.Entry(entityToDelete).State == EntityState.Detached) {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updating order in the database
        /// </summary>
        /// <param name="id">The identifier of the updated order</param>
        /// <param name="entityToUpdate">A new order to be retrieved after the update</param>
        public virtual void Update(object id, TEntity entityToUpdate) {
            TEntity entity = GetByID(id);
            _context.Entry(entity).CurrentValues.SetValues(entityToUpdate);
            _context.SaveChanges();

        }

        public virtual void Update(TEntity entity) {
            _dbSet.AddOrUpdate(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Making changes at the order in the database (saves changes at once)
        /// </summary>
        /// <param name="entity">An order that will be used for updating</param>
        public void Edit(TEntity entity) {
            try {
                if (entity == null) {
                    throw new ArgumentNullException("entity");
                }
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx) {

            }
        }
    }
}
