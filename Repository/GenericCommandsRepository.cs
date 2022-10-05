using CustomerManagement.Data;
using CustomerManagement.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace CustomerManagement.Repository
{
    public class GenericCommandsRepository<T> 
        : IGenericCommandsRepository<T> where T : class
    {
        private IDbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;

        public GenericCommandsRepository(IUnitOfWork<DBEntities> unitOfWork)
            : this(unitOfWork.Context) {}

        public GenericCommandsRepository(DBEntities _context)
        {
            _isDisposed = false;
            Context = _context;
        }

        public DBEntities Context { get; set; }

        public virtual IQueryable<T> Table { get { return Entities; } }

        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = Context.Set<T>()); }
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }

        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Add(entity);

                if (Context == null || _isDisposed)
                    Context = new DBEntities();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors
                        .ValidationErrors)
                        _errorMessage += string.Format(
                            "Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage)
                            + Environment.NewLine;

                throw new Exception(_errorMessage, dbEx);
            }
        }

        public void BulkInsert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }
                Context.Configuration.AutoDetectChangesEnabled = false;
                Context.Set<T>().AddRange(entities);
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors
                        .ValidationErrors)
                    {
                        _errorMessage += string.Format(
                            "Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage) 
                            + Environment.NewLine;
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                if (Context == null || _isDisposed)
                    Context = new DBEntities();

                SetEntryModified(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors
                        .ValidationErrors)
                        _errorMessage += Environment.NewLine
                            + string.Format("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);

                throw new Exception(_errorMessage, dbEx);
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                if (Context == null || _isDisposed)
                    Context = new DBEntities();

                Entities.Remove(entity);
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors
                        .ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format(
                            "Property: {0} Error: {1}", 
                            validationError.PropertyName,
                            validationError.ErrorMessage);

                throw new Exception(_errorMessage, dbEx);
            }
        }
        
        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

    }
}
