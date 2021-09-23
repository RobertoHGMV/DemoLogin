using DemoLogin.Domain.Models;
using DemoLogin.Domain.Repositories;
using DemoLogin.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoLogin.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<Product>> GetAll(string userId)
        {
            return await _context.Products.Where(c => c.UserId == userId).AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
