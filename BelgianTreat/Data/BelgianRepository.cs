using BelgianTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelgianTreat.Data
{
    public class BelgianRepository : IBelgianRepository
    {
        private readonly BelgianContext _ctx;
        private readonly ILogger<BelgianRepository> _logger;

        public BelgianRepository(BelgianContext ctx, ILogger<BelgianRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");

                return _ctx.Products
                           .OrderBy(p => p.Title)
                           .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                       .Include(o => o.Items)
                       .ThenInclude(i => i.Product)
                       .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders
                       .Include(o => o.Items)
                       .ThenInclude(i => i.Product)
                       .Where(o => o.Id == id)
                       .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                       .Where(p => p.Category == category)
                       .ToList();
        }

        public bool SaveAll()
        {
            // Check if something changed before saving (> 0)
            return _ctx.SaveChanges() > 0;
        }
    }
}
