using CustomerManagement.Data;
using System;
using System.Collections.Generic;

namespace CustomerManagement
{
    class Program
    {
        static void Main()
        {
            Controller.Controller controller = new Controller.Controller();

            /* ---------- Customer ---------- */
            // Create
            Customer natalie = new Customer()
            {
                Firstname = "Nataly",
                Lastname = "Portman",
                Address = "32 Valhala Ave.",
                PostalCode = "SW3 1HZ"
            };
            controller.AddCustomer(natalie);

            Customer hugh = new Customer()
            {
                Firstname = "Hugh",
                Lastname = "Jackman",
                Address = "16 Melrose Ave.",
                PostalCode = "NW3 1HZ"
            };
            controller.AddCustomer(hugh);

            // Update
            natalie.Firstname = "Natalie";
            controller.UpdateCustomer(natalie);

            // Read
            Customer x = controller.findCustomerById(natalie.Id);
            Customer y = controller.findCustomerById(5);

            // Delete
            //controller.DeleteCustomer(controller.findCustomerById(natalie.Id));
            //controller.DeleteCustomer(controller.findCustomerById(hugh.Id));
            /* ------------------------------ */


            /* ---------- Order ---------- */
            // Create
            Order computerOrder = new Order()
            {
                OrderDate = new DateTime(2022, 10, 05),
                Customer = natalie
            };
            controller.AddOrder(computerOrder);

            Order bicycleOrder = new Order()
            {
                OrderDate = new DateTime(2022, 10, 02),
                Customer = natalie
            };
            controller.AddOrder(bicycleOrder);

            // Update
            //computerOrder.OrderDate = new DateTime(2022, 10, 01);
            //controller.UpdateOrder(computerOrder);
            //computerOrder.Customer = hugh;
            //controller.UpdateOrder(computerOrder);

            // Read
            Order aOrder = controller.FindOrderById(computerOrder.Id);

            // Delete
            //controller.DeleteOrder(controller.findOrderById(computerOrder.Id));
            /* --------------------------- */


            /* ---------- Item ---------- */
            // Create
            Item mainComputerItem = new Item()
            {
                Order = computerOrder,
                ProductName = "Intel Pentium IV",
                ProductPrice = Convert.ToDecimal(500.00),
                Quantity = 1
            };
            controller.AddItem(mainComputerItem);

            Item screenItem = new Item()
            {
                Order = computerOrder,
                ProductName = "Samsung",
                ProductPrice = Convert.ToDecimal(300.00),
                Quantity = 2
            };
            controller.AddItem(screenItem);

            Item bicycleSkeletonItem = new Item()
            {
                Order = bicycleOrder,
                ProductName = "Ideal Zig-zag",
                ProductPrice = Convert.ToDecimal(500.00),
                Quantity = 1
            };
            controller.AddItem(bicycleSkeletonItem);

            Item bicycleBrakeItem = new Item()
            {
                Order = bicycleOrder,
                ProductName = "Shimano breaks",
                ProductPrice = Convert.ToDecimal(50.50),
                Quantity = 2
            };
            controller.AddItem(bicycleBrakeItem);

            // Update
            mainComputerItem.ProductPrice = Convert.ToDecimal(600.00);
            controller.UpdateItem(mainComputerItem);
            screenItem.ProductPrice = Convert.ToDecimal(250.00);
            controller.UpdateItem(screenItem);
            screenItem.Quantity = 4;
            controller.UpdateItem(screenItem);

            // Read
            Item aItem = controller.FindItemById(mainComputerItem.Id);
            Item bItem = controller.FindItemById(screenItem.Id);

            // Delete
            //controller.DeleteItem(aItem);
            //controller.DeleteItem(bItem);

            /* -------------------------- */

            /* ---------- Query ---------- */
            List<Order> orders = controller.GetCustomersOrdersByOrderDate(natalie);
            /* --------------------------- */

        }
    }
}
