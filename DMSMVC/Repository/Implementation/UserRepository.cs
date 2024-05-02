using DMSMVC.Context;
using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DMSMVC.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DmsContext _context;
        public UserRepository(DmsContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User type)
        {
			await _context.Users.AddAsync(type);
            return type;
        }

        public void Delete(User type)
        {
            _context.Users.Remove(type);
        }


        public async Task<User> GetAsync(Expression<Func<User, bool>> exp)
        {
            var user = await _context.Users.Include(a => a.Staff)
                .ThenInclude(a => a.Department)
                .ThenInclude(a => a.Documents).FirstOrDefaultAsync(exp);
            return user;
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public bool IsExist(string userEmail)
        {
            return _context.Users.Any(a => a.Email == userEmail);
        }

        public User Update(User user)
        {
            _context.Users.Update(user);
            return user;
        }


    }
}
