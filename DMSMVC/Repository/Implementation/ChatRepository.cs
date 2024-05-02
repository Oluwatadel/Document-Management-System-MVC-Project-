using DMSMVC.Context;
using DMSMVC.Models.Entities;
using DMSMVC.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DMSMVC.Repository.Implementation
{
	public class ChatRepository : IChatRepository
    {
        private readonly DmsContext _context;
        public ChatRepository(DmsContext context)
        {
            _context = context;
        }
        public async Task<Chat> CreateAsync(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
            return chat;
        }

        public void Delete(Chat type)
        {
            _context.Chats.Remove(type);
        }

        public async Task<Chat> GetAsync(string email)
        {
            var chat = await _context.Chats
                .Include(a => a.ChatContents)
                .FirstOrDefaultAsync(a => a.ReceiverEmail == email || a.SenderEmail == email);
            return chat;
        }

        public async Task<Chat> GetAsync(Expression<Func<Chat, bool>> exp)
        {
            var chat = await _context.Chats
                .Include(a => a.ChatContents).FirstOrDefaultAsync(exp);
            return chat;
        }

        public async Task<IList<Chat>> GetAllAsync()
        {
            return await _context.Chats.ToListAsync();
        }
	}
}
