using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Repository
{
    class ItemRepository : IItemRepository
    {
        private readonly DBEntities dBEntities;
        private bool disposed;

        public ItemRepository()
        {
            dBEntities = new DBEntities();
        }

        public ItemRepository(DBEntities _dBEntities)
        {
            dBEntities = _dBEntities;
        }

        IEnumerable<Item> IItemRepository.GetAll()
        {
            return dBEntities.Items.ToList();
        }

        Item IItemRepository.GetById(int ItemtID)
        {
            return dBEntities.Items.Find(ItemtID);
        }

        void IItemRepository.Insert(Item item)
        {
            dBEntities.Items.Add(item);
        }

        void IItemRepository.Update(Item item)
        {
            dBEntities.Entry(item).State = EntityState.Modified;
        }

        void IItemRepository.Delete(int ItemID)
        {
            Item item = dBEntities.Items.Find(ItemID);
            dBEntities.Items.Remove(item);
        }

        void IItemRepository.Save()
        {
            dBEntities.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dBEntities.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
