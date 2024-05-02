using DMSMVC.Context;
using DMSMVC.Models.Entities;
using DMSMVC.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DMSMVC.Repository.Implementation
{

    public class DepartmentRepository : IDepartmentRepository
    {
        public readonly DmsContext _context;
        public DepartmentRepository(DmsContext context)
        {
            _context = context;
        }
        public async Task<Department> CreateAsync(Department type)
        {
            await _context.Departments.AddAsync(type);
            return type;
        }

        public void Delete(Department type)
        {
            _context.Departments.Remove(type);
        }

        public async Task<Department> GetAsync(Expression<Func<Department, bool>> exp)
        {
            var department = await _context.Departments
                .Include(a => a.Staffs)
                .ThenInclude(a => a.User)
                .Include(a => a.Documents).FirstOrDefaultAsync(exp);
            return department;
        }

        public async Task<IList<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public Department UpdateDepartment(Department type)
        {
            _context.Departments.Update(type);
            return type;
        }
    }
}