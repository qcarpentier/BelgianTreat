using System.Collections.Generic;
using BelgianTreat.Data.Entities;

namespace BelgianTreat.Data
{
    public interface IBelgianRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);

        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(int id);

        bool SaveAll();
        void AddEntity(object model);
    }
}