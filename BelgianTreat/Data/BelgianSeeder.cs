using BelgianTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelgianTreat.Data
{
    public class BelgianSeeder
    {
        private readonly BelgianContext _context;
        private readonly IHostingEnvironment _hosting;

        public BelgianSeeder(BelgianContext context, IHostingEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }
        public void Seed()
        {
            // Double check if the db is created (avoid some critical errors)
            _context.Database.EnsureCreated();

            if (!_context.Products.Any())
            {
                // Create sample datas

                // Products
                var filePath = Path.Combine(_hosting.ContentRootPath, "Data/sample.json");
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _context.Products.AddRange(products);

                // Order and OrderItem
                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };
                _context.Orders.Add(order);

                _context.SaveChanges();
            }
        }
    }
}
