using CustomerManagement.Data;
using CustomerManagement.Repository;
using CustomerManagement.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

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
        }

        // ---------- API calls ---------- //

        /// <summary>
        /// Add a new Customer into the database. 
        /// <para>Customer must have Firstname, Lastname, Address
        /// and Postcode attributes. If either one is missing nothing is added
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
        public void UpdateCustomer(Customer _customer)
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
        public void DeleteCustomer(Customer _customer)
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
        /// <para>If customer ID does not match an ID in the database,
        /// null is returned</para>
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
        public void AddOrder(Order _order)
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
        public void UpdateOrder(Order _order)
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
        public void DeleteOrder(Order _order)
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
        /// <para>If Order ID does not match an ID in the database,
        /// null is returned</para>
        /// </summary>
        /// <param name="_orderId"></param>
        /// <returns>Order</returns>
        public Order FindOrderById(int _orderId)
        {
            return orderQueriesRepository.GetById(_orderId);
        }


        /// <summary>
        /// Add an Item to the database.
        /// <para>The Item must have a Product Name, a Product Price and 
        /// a Quantity attributes. If either one is missing, or has an illegal
        /// value, nothing is added to the database.</para>
        /// <para> - Quantity must be at least 1.</para>
        /// <para> - Product Price must be greater or equal to zero.</para>
        /// <para>Adding an Item updates the Final Price of the Order
        /// the Item belongs to</para>
        /// </summary>
        /// <param name="_item"></param>
        public void AddItem(Item _item)
        {
            // Prevent illegal values
            if (_item.Quantity < 1 || _item.ProductPrice < 0)
                return;

            try
            {
                unitOfWork.CreateTransaction();
                
                // First insert item
                itemCommandsRepository.Insert(_item);

                // Then update order's final price
                Order order = _item.Order;
                order.TotalPrice += _item.ProductPrice * _item.Quantity;
                orderCommandsRepository.Update(order);

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
        ///<para>Updating an Item updates the Final Price of the Order
        /// the Item belongs to</para>
        /// </summary>
        /// <param name="_item"></param>
        public void UpdateItem(Item _item)
        {
            try
            {
                unitOfWork.CreateTransaction();

                // First update item
                itemCommandsRepository.Update(_item);
                
                // Then update order's final price
                Order order = _item.Order;
                decimal totalPrice = Convert.ToDecimal(0.0);
                foreach (Item item in order.Items)
                    if (item.Id == _item.Id)
                        totalPrice += _item.ProductPrice * _item.Quantity;
                    else
                        totalPrice += item.ProductPrice * item.Quantity;
                order.TotalPrice = totalPrice;
                
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
        ///<para>Deleting an Item updates the Final Price of the Order
        /// the Item belongs to</para>
        /// </summary>
        /// <param name="_item"></param>
        public void DeleteItem(Item _item)
        {
            try
            {
                unitOfWork.CreateTransaction();

                // Save item's order, price and quantity
                Order order = _item.Order;
                decimal itemsPrice = _item.ProductPrice;
                int itemsQuantity = _item.Quantity;

                // FIrst delete item
                itemCommandsRepository.Delete(_item);

                // Then update order's final price
                order.TotalPrice -= itemsPrice * itemsQuantity;
                orderCommandsRepository.Update(order);

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
        /// <para>If Item ID does not match an ID in the database,
        /// null is returned</para>
        /// </summary>
        /// <param name="_itemId"></param>
        /// <returns>Item</returns>
        public Item FindItemById(int _itemId)
        {
            return itemQueriesRepository.GetById(_itemId);
        }

        /// <summary>
        /// Return a list of Orders by Customer _customer sorted by Order Date 
        /// in ascending order.
        /// </summary>
        /// <param name="_customer"></param>
        /// <returns></returns>
        public List<Order> GetCustomersOrdersByOrderDate(Customer _customer)
        {
            List<Order> orders = orderQueriesRepository
                .GetByCustmerIDByOrderDate(_customer.Id).ToList();
            return orders;
        }
    }
}
