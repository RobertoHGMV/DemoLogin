using DemoLogin.Domain.Models;
using DemoLogin.Domain.Repositories;
using DemoLogin.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoLogin.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<User>> GetAll()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetById(string id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User> GetByUserName(string userName)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(c => c.UserName == userName);
        }

        public async Task Add(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
