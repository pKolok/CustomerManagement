using CustomerManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement
{
    class Program
    {
        //static void Main(string[] args)
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
            controller.updateCustomer(natalie);

            // Read
            Customer x = controller.findCustomerById(natalie.Id);
            Customer y = controller.findCustomerById(hugh.Id);

            // Delete
            //controller.deleteCustomer(controller.findCustomerById(natalie.Id));
            //controller.deleteCustomer(controller.findCustomerById(hugh.Id));
            /* ------------------------------ */


            /* ---------- Order ---------- */
            // Create
            Order computerOrder = new Order()
            {
                OrderDate = new DateTime(2022, 10, 03),
                Customer = natalie,
            };
            controller.addOrder(computerOrder);

            // Update
            computerOrder.OrderDate = new DateTime(2022, 10, 04);
            controller.updateOrder(computerOrder);

            // Read
            Order z = controller.findOrderById(computerOrder.Id);

            // Delete
            //controller.deleteOrder(controller.findOrderById(computerOrder.Id));
            /* --------------------------- */


            /* ---------- Item ---------- */
            Item mainComputerItem = new Item()
            {
                Order = computerOrder,
                //OrderID = computerOrder.Id
            };
            controller.addItem(mainComputerItem);

            Item screenItem = new Item()
            {
                Order = computerOrder,
                //OrderID = computerOrder.Id
            };
            controller.addItem(screenItem);

            /* -------------------------- */


            /* ---------- Product ---------- */
            Product mainComputerProduct = new Product()
            {
                Name = "Intel Pentium IV",
                Price = Convert.ToDecimal(659.99),
                Item = mainComputerItem
            };
            controller.addProduct(mainComputerProduct);

            Product screenProduct = new Product()
            {
                Name = "Samsung",
                Price = Convert.ToDecimal(299.90),
                Item = screenItem
            };
            /* ----------------------------- */

        }
    }
}
