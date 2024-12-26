using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatSyncPVT.Models;
using Microsoft.EntityFrameworkCore;
using static ChatSyncPVT.Models.MessageModels;

public class MessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveMessageAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Message>> GetMessagesByGroupAsync(int groupId)
    {
        return await _context.Messages
            .Where(m => m.GroupId == groupId)
            .OrderBy(m => m.SentAt)
            .ToListAsync();
    }
}
