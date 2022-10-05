using CustomerManagement.Data;
using CustomerManagement.Repository;
using CustomerManagement.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Controller
{
    class Controller
    {
        private UnitOfWork<DBEntities> unitOfWork 
            = new UnitOfWork<DBEntities>();
        private GenericQueriesRepository<Customer> customerQueriesRepository;
        private GenericCommandsRepository<Customer> customerCommandsRepository;
        private GenericQueriesRepository<Order> orderQueriesRepository;
        private GenericCommandsRepository<Order> orderCommandsRepository;
        private GenericQueriesRepository<Item> itemQueriesRepository;
        private GenericCommandsRepository<Item> itemCommandsRepository;
        private GenericQueriesRepository<Product> productQueriesRepository;
        private GenericCommandsRepository<Product> productCommandsRepository;

        public Controller()
        {
            // Use Generic Repository with Unit of work
            customerQueriesRepository 
                = new GenericQueriesRepository<Customer>(unitOfWork);
            customerCommandsRepository 
                = new GenericCommandsRepository<Customer>(unitOfWork);
            orderQueriesRepository 
                = new GenericQueriesRepository<Order>(unitOfWork);
            orderCommandsRepository 
                = new GenericCommandsRepository<Order>(unitOfWork);
            itemQueriesRepository 
                = new GenericQueriesRepository<Item>(unitOfWork);
            itemCommandsRepository 
                = new GenericCommandsRepository<Item>(unitOfWork);
            productQueriesRepository 
                = new GenericQueriesRepository<Product>(unitOfWork);
            productCommandsRepository 
                = new GenericCommandsRepository<Product>(unitOfWork);
        }

        // ---------- API calls ---------- //

        /// <summary>
        /// Add a new Customer into the database. 
        /// <para>Customer must have Firstname, Lastname, Address
        /// and Postcode attributes. If either one is missing nothing is added.
        /// to the database </para>
        /// </summary>
        /// <param name="_customer"></param>        
        public void AddCustomer(Customer _customer)
        {
            try
            {
                unitOfWork.CreateTransaction();
                customerCommandsRepository.Insert(_customer);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Update an existing Customer in the database.
        /// <para>Customer must exist in the database. If it does not,
        /// nothing happens.</para>
        /// </summary>
        /// <param name="_customer"></param>
        public void updateCustomer(Customer _customer)
        {
            try
            {
                unitOfWork.CreateTransaction();
                customerCommandsRepository.Update(_customer);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Delete provided Customer from the database. 
        /// <para>Customer must exist in the database. If it does not, 
        /// nothing is deleted.</para>
        /// </summary>
        /// <param name="_customer"></param>
        public void deleteCustomer(Customer _customer)
        {
            try
            {
                unitOfWork.CreateTransaction();
                customerCommandsRepository.Delete(_customer);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Returns a Customer from the Customer ID.
        /// </summary>
        /// <param name="_customerId"></param>
        /// <returns>Customer</returns>
        public Customer findCustomerById(int _customerId)
        {
            return customerQueriesRepository.GetById(_customerId);
        }


        /// <summary>
        /// Add an Order to the database.
        /// <para>The Order must have an Order Date and a Customer as 
        /// attributes. If either one is missing nothing is 
        /// added to the database.</para>
        /// </summary>
        /// <param name="_order"></param>
        public void addOrder(Order _order)
        {
            // If _order does not have an OrderDate assigned the min is given.
            if (_order.OrderDate == DateTime.MinValue)
                return;

            try
            {
                unitOfWork.CreateTransaction();
                orderCommandsRepository.Insert(_order);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Update an Order in the database.
        /// <para>The Order must exist in the database. If it does not,
        /// nothing happens.</para>
        /// </summary>
        /// <param name="_order"></param>
        public void updateOrder(Order _order)
        {
            try
            {
                unitOfWork.CreateTransaction();
                orderCommandsRepository.Update(_order);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Delete provided Order from the database.
        /// <para>Order must exist in the database. If it does not, 
        /// nothing is deleted.</para>
        /// </summary>
        /// <param name="_order"></param>
        public void deleteOrder(Order _order)
        {
            try
            {
                unitOfWork.CreateTransaction();
                orderCommandsRepository.Delete(_order);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Returns an Order from the Order ID.
        /// </summary>
        /// <param name="_orderId"></param>
        /// <returns>Order</returns>
        public Order findOrderById(int _orderId)
        {
            return orderQueriesRepository.GetById(_orderId);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_item"></param>
        public void addItem(Item _item)
        {
            try
            {
                unitOfWork.CreateTransaction();
                itemCommandsRepository.Insert(_item);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Update an Item in the database.
        /// <para>The Item must exist in the database. If it does not,
        /// nothing happens.</para>
        /// </summary>
        /// <param name="_item"></param>
        public void updateItem(Item _item)
        {
            try
            {
                unitOfWork.CreateTransaction();
                itemCommandsRepository.Update(_item);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Delete provided Item from the database.
        /// <para>Item must exist in the database. If it does not, 
        /// nothing is deleted.</para>
        /// </summary>
        /// <param name="_item"></param>
        public void deleteItem(Item _item)
        {
            try
            {
                unitOfWork.CreateTransaction();
                itemCommandsRepository.Delete(_item);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Returns an Item from the Item ID.
        /// </summary>
        /// <param name="_itemId"></param>
        /// <returns>Item</returns>
        public Item findItemById(int _itemId)
        {
            return itemQueriesRepository.GetById(_itemId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_product"></param>
        public void addProduct(Product _product)
        {
            try
            {
                unitOfWork.CreateTransaction();
                productCommandsRepository.Insert(_product);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Update an Product in the database.
        /// <para>The Product must exist in the database. If it does not,
        /// nothing happens.</para>
        /// </summary>
        /// <param name="_product"></param>
        public void updateProduct(Product _product)
        {
            try
            {
                unitOfWork.CreateTransaction();
                productCommandsRepository.Update(_product);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Delete provided Product from the database.
        /// <para>Product must exist in the database. If it does not, 
        /// nothing is deleted.</para>
        /// </summary>
        /// <param name="_product"></param>
        public void deleteProduct(Product _product)
        {
            try
            {
                unitOfWork.CreateTransaction();
                productCommandsRepository.Delete(_product);
                unitOfWork.Save();
                unitOfWork.Commit();
            }
            catch (Exception)
            {
                unitOfWork.Rollback();
            }
        }

        /// <summary>
        /// Returns a Product from the Product ID.
        /// </summary>
        /// <param name="_productId"></param>
        /// <returns>Product</returns>
        public Product findProductById(int _productId)
        {
            return productQueriesRepository .GetById(_productId);
        }
    }
}
