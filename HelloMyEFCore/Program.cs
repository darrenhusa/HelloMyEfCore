using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloMyEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            using (var db = new OrderContext())
            {
                /* var p = new Product();
                p.Title = "Widget";
                p.Price = 20d;
                db.Products.Add(p);

                var o = new Order();
                o.OrderItems = new List<OrderItem>();
                o.CustomerName = "BOB";
                o.OrderDate = DateTime.Now;

                var oi1 = new OrderItem();
                oi1.Order = o;
                oi1.Product = p;
                oi1.SalePrice = 19d;
                oi1.Quantity = 2;

                o.OrderItems.Add(oi1);

                db.Orders.Add(o);

                db.SaveChanges();
                */

                //var orders = db.Orders
                //    .Include(order => order.OrderItems)
                //    .ThenInclude(orderItem => orderItem.Product);

                //foreach (var order in orders)
                //{
                //    Console.WriteLine($"{order.OrderDate} - {order.CustomerName}");

                //    foreach (var orderItem in order.OrderItems)
                //    {
                //        Console.WriteLine($"     ({orderItem.Product.Title}) {orderItem.Quantity} * {orderItem.SalePrice}");
                //    }
                //}

                /*
                 var p1 = new Product { Title = "Gadget", Price = 40d };
                 db.Products.Add(p1);

                 var p2 = new Product { Title = "Fiz", Price = 10d };
                 db.Products.Add(p2);

                 var c = new Customer { CustomerName = "Richard", StreetAddress = "123 E Elm St." };
                 db.Customers.Add(c);

                 var o1 = new Order { Customer = c, OrderDate = DateTime.Now, OrderItems = new List<OrderItem>() };

                 var oi1 = new OrderItem { Order = o1, Product = p1, Quantity = 3, SalePrice = 39d };
                 var oi2 = new OrderItem { Order = o1, Product = p2, Quantity = 1, SalePrice = 9d };

                 o1.OrderItems.Add(oi1);
                 o1.OrderItems.Add(oi2);
                 db.Orders.Add(o1);

                 db.SaveChanges();
            */
                /*
                    var orders = db.Orders
                        .Where(p => p.OrderDate > DateTime.Now.AddHours(-6))
                        .Include(order => order.Customer)
                        .Include(order => order.OrderItems)
                        .ThenInclude(orderItem => orderItem.Product);

                    foreach (var order in orders)
                    {
                        Console.WriteLine($"{order.OrderDate} - {order.Customer.CustomerName}");

                        foreach (var orderItem in order.OrderItems)
                        {
                            Console.WriteLine($"     ({orderItem.Product.Title}) {orderItem.Quantity} * {orderItem.SalePrice}");
                        }
                    }
                    */
                var orders = db.Orders
                    .Include(order => order.Customer)
                    .Include(order => order.OrderItems)
                    .ThenInclude(orderItem => orderItem.Product);

                foreach (var order in orders)
                {
                    Console.WriteLine($"{order.OrderDate} -" +
                        $"{order.Customer.CustomerName}");

                    foreach (var orderItem in order.OrderItems)
                    {
                        Console.WriteLine($"     ({orderItem.Product.Title}) {orderItem.Quantity} * {orderItem.SalePrice}");
                    }
                }

            }

            Console.WriteLine("Finished!");
            Console.ReadLine();
        }
    }

    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=HelloMyEFCore;Trusted_Connection=True;");
        }
    }

    [Table(name: "Customer")]
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string StreetAddress { get; set; }

    }

    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        
        public Customer Customer { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public double SalePrice { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }


    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
    }

}
