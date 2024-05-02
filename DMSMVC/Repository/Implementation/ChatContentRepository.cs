using DMSMVC.Context;
using DMSMVC.Models.Entities;
using DMSMVC.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace DMSMVC.Repository.Implementation
{
    public class ChatContentRepository : IChatContentRepository
    {

        public readonly DmsContext _context;
        public readonly ChatFileContext _chatFileContext;
        public ChatContentRepository(DmsContext context, ChatFileContext chatFileContext)
        {
            _chatFileContext = chatFileContext;
            _context = context;

        }
        public async Task<ChatContent> CreateAsync(ChatContent type)
        {
            await _context.ChatContents.AddAsync(type);
            return type;
        }

        public void Delete(ChatContent type)
        {
            _context.ChatContents.Remove(type);
        }

        public async Task<ChatContent> GetAsync(Expression<Func<ChatContent, bool>> exp)
        {
            var chat = await _context.ChatContents.FirstOrDefaultAsync(exp);
            return chat;
        }

        public async Task<IList<ChatContent>> GetAllAsync()
        {
            return await _context.ChatContents.ToListAsync();
        }

        public async Task<List<ChatContent>> GetChatContents(Guid chatId)
        {
            var contents = await _context.ChatContents.Where(a => a.ChatId == chatId).ToListAsync();
            return contents;
        }
    }
}
