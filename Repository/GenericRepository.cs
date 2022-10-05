using CustomerManagement.Data;
using CustomerManagement.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //private DBEntities dbEntities = null;
        //private DbSet<T> table = null;
        private IDbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;

        //public GenericRepository()
        //{
        //    this.dbEntities = new DBEntities();
        //    table = dbEntities.Set<T>();
        //}
        public GenericRepository(IUnitOfWork<DBEntities> unitOfWork)
            : this(unitOfWork.Context)
        {
        }

        //public GenericRepository(DBEntities _context)
        //{
        //    this.dbEntities = _context;
        //    table = _context.Set<T>();
        //}
        public GenericRepository(DBEntities _dBEntities)
        {
            _isDisposed = false;
            Context = _dBEntities;
        }

        public DBEntities Context { get; set; }

        public virtual IQueryable<T> Table
        {
            get { return Entities; }
        }

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

        public IEnumerable<T> GetAll()
        {
            //return table.ToList();
            return Entities.ToList();
        }

        public T GetById(object id)
        {
            //return table.Find(id);
            return Entities.Find(id);
        }

        //public void Insert(T obj)
        //{
        //    table.Add(obj);
        //}

        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                Entities.Add(entity);
                if (Context == null || _isDisposed)
                    Context = new DBEntities();
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be 
                //called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}",
                            validationError.PropertyName, validationError.ErrorMessage) 
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
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage += string.Format("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }

        //public void Update(T obj)
        //{
        //    table.Attach(obj);
        //    dbEntities.Entry(obj).State = EntityState.Modified;
        //}

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new DBEntities();
                SetEntryModified(entity);
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }

        //public void Delete(object id)
        //{
        //    T existing = table.Find(id);
        //    table.Remove(existing);
        //}

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new DBEntities();
                Entities.Remove(entity);
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format(
                            "Property: {0} Error: {1}", validationError.PropertyName,
                            validationError.ErrorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }

        //public void Save()
        //{
        //    dbEntities.SaveChanges();
        //}

        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

    }
}
