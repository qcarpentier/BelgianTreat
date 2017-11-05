using System.Collections.Generic;
using BelgianTreat.Data.Entities;

namespace BelgianTreat.Data
{
    public interface IBelgianRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
    }
}