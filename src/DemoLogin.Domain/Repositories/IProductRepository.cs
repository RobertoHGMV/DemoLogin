using DemoLogin.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoLogin.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAll(string userId);
        Task<Product> GetById(int id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
