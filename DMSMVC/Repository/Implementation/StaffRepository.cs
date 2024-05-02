using DMSMVC.Context;
using DMSMVC.Models.Entities;
using DMSMVC.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DMSMVC.Repository.Implementation
{

    public class StaffRepository : IStaffRepository
    {
        private readonly DmsContext _context;
        public StaffRepository(DmsContext context)
        {
            _context = context;
        }
        public async Task<Staff> CreateAsync(Staff type)
        {
            await _context.Staffs.AddAsync(type);
            return type;
        }

        public void Delete(Staff type)
        {
            _context.Staffs.Remove(type);
        }

        public async Task<Staff> GetAsync(Expression<Func<Staff, bool>> exp)
        { 
            var staff = await _context.Staffs.Include(a => a.User)
                .Include(a => a.Department).
                ThenInclude(a => a.Documents)
                .FirstOrDefaultAsync(exp);
            return staff;
        }

        public async Task<IList<Staff>> GetAllAsync()
        {
            return await _context.Staffs.ToListAsync();
        }

        public Staff Update(Staff staff)
        {
            _context.Staffs.Update(staff);
            return staff;
        }
    }
}