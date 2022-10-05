using CustomerManagement.Data;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace CustomerManagement.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>,
        IDisposable where TContext : DBEntities, new()
    {
        //TContext is the DBEntities class
        private readonly TContext context;
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private DbContextTransaction _objTran;

        public UnitOfWork()
        {
            context = new TContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public TContext Context { get { return context; } }

        public void CreateTransaction()
        {
            _objTran = context.Database.BeginTransaction();
        }
        
        public void Commit()
        {
            _objTran.Commit();
        }

        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }
       
        public void Save()
        {
            try
            {
                context.SaveChanges();
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
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    context.Dispose();
            _disposed = true;
        }
    }
}
