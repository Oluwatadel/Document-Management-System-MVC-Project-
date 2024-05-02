using DMSMVC.Models.Entities;

namespace DMSMVC.Repository.Interface
{
    public interface IChatContentRepository : IBaseRepository<ChatContent>
    {
        Task<List<ChatContent>> GetChatContents(Guid id);
    }
}
