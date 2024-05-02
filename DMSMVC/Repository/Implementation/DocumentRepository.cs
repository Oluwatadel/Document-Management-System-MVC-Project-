using DMSMVC.Context;
using DMSMVC.Models.Entities;
using DMSMVC.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DMSMVC.Repository.Implementation
{
	public class DocumentRepository : IDocumentRepository
    {
        private readonly DmsContext _context;

        public DocumentRepository(DmsContext context)
        {
            _context = context;
        }

        public async Task<Document> CreateAsync(Document type)
        {
            await _context.Documents.AddAsync(type);
            return type;
        }

        public void Delete(Document type)
        {
            _context.Documents.Remove(type);
        }

        public async Task<IList<Document>> GetAllAsync()
        {
            return await _context.Documents.ToListAsync();
        }

        public Document UpdateDocument(Document type)
        {
            _context.Documents.Update(type);
            return type;
        }

		public async Task<Document?> GetAsync(Expression<Func<Document, bool>> exp)
		{
			var document = await _context.Documents.Include(a => a.Department).FirstOrDefaultAsync(exp);
			return document == null ? null : document;
		}
	}
}

