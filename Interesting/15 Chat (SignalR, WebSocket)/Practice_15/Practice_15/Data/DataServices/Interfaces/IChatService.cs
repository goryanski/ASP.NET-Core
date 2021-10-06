using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Practice_15.Data.Entity;
using Practice_15.Models;

namespace Practice_15.Data.DataServices.Interfaces
{
    public interface IChatService
    {
        void DbInit();
        List<ChatViewModel> GetUserChats(int userId);
        List<ChatMessage> GetChatMessages(int chatId);
        void AddNewMessageInDB(ChatMessage message);
        void CreateNewChat(int chatCreatorId, int invitedUserId, string groupName);
        ChatViewModel GetNewChatFromDb();
    }
}
