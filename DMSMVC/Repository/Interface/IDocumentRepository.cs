
using DMSMVC.Models.Entities;

namespace DMSMVC.Repository.Interface
{
    public interface IDocumentRepository : IBaseRepository<Document>
    {
        public Document UpdateDocument(Document type);
    }
}
