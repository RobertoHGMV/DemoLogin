using DemoLogin.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoLogin.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IList<User>> GetAll();
        Task<User> GetById(string id);
        Task<User> GetByUserName(string userName);
        Task Add(User user);
        Task Update(User user);
        Task Delete(User user);
    }
}
