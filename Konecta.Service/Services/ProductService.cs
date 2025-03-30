using Konecta.Core.Models;
using Konecta.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Konecta.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Price = 1200, Description = "High-end gaming laptop" },
            new Product { Id = 2, Name = "Smartphone", Price = 800, Description = "Latest Android device" },
            new Product { Id = 3, Name = "Headphones", Price = 150, Description = "Noise-canceling headphones" }
        };

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task<Product> AddProductAsync(Product product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
            return Task.FromResult(product);
        }

        public Task<bool> UpdateProductAsync(Product product)
        {
            var existing = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existing == null) return Task.FromResult(false);

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Description = product.Description;

            return Task.FromResult(true);
        }

        public Task<bool> DeleteProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return Task.FromResult(false);

            _products.Remove(product);
            return Task.FromResult(true);
        }
    }
}
