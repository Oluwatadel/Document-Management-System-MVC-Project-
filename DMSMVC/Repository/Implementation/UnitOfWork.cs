using DMSMVC.Context;
using DMSMVC.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DMSMVC.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DmsContext _context;

        public UnitOfWork(DmsContext context)
        {
            _context = context;
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
