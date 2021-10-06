using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Practice_15.Data.DataServices.Interfaces;
using Practice_15.Data.Entity;
using Practice_15.Models;

namespace Practice_15.Data.DataServices
{
    public class ChatService : IChatService
    {
        ApplicationDbContext db;
        IAccountsService accountsService;
        public ChatService(ApplicationDbContext context, IAccountsService accountsService)
        {
            db = context;
            this.accountsService = accountsService;
        }

        public void AddNewMessageInDB(ChatMessage message)
        {
            Message newMessage = new Message
            {
                Sender = message.Sender,
                Text = message.Text,
                ChatId = message.ChatId
            };
            db.Messages.Add(newMessage);
            db.SaveChanges();
        }

        public void CreateNewChat(int chatCreatorId, int invitedUserId, string groupName)
        {
            User userCreator = accountsService.GetUserById(chatCreatorId);
            User invitedUser = accountsService.GetUserById(invitedUserId);
            Chat newChat = new Chat
            {
                Name = groupName,
                Messages = new List<Message>(),
                Users = new List<User> { userCreator, invitedUser }
            };
            db.Chats.Add(newChat);
            db.SaveChanges();
        }

        public void DbInit()
        {
            Message message1 = new Message { Sender = "admin", Text = "Hello, this is a chat for all authorized users, pleasant communication!" };

            Chat chat1 = new Chat { Name = "General", Messages = new List<Message> { message1 } };
            db.Chats.Add(chat1);
            db.SaveChanges();

            User user1 = new User { Email = "vasya1@gmail.com", Password = "123456", Chats = new List<Chat> { chat1 } };
            User user2 = new User { Email = "vasya2@gmail.com", Password = "123456", Chats = new List<Chat> { chat1 } };
            User user3 = new User { Email = "vasya3@gmail.com", Password = "123456", Chats = new List<Chat> { chat1 } };
            db.Users.Add(user1);
            db.Users.Add(user2);
            db.Users.Add(user3);
            db.SaveChanges();
        }

        public List<ChatMessage> GetChatMessages(int chatId)
        {
            var srchChat = db.Chats.Include(ch => ch.Messages)
                .Where(ch => ch.Id == chatId)
                .First();
            var chatMessages = srchChat.Messages;
            List<ChatMessage> messagesViews = new List<ChatMessage>();
            foreach (var message in srchChat.Messages)
            {
                messagesViews.Add(new ChatMessage 
                {
                    ChatId = message.ChatId,
                    Sender = message.Sender,
                    Text = message.Text
                });
            }
            return messagesViews;
        }

        public ChatViewModel GetNewChatFromDb()
        {
            Chat latestChat = db.Chats
                .OrderByDescending(ch => ch.Id)
                .FirstOrDefault();
            return new ChatViewModel
            {
                Id = latestChat.Id,
                Name = latestChat.Name,
                Messages = new List<MessageViewModel>() // new chat hasn't any messages
            };
        }

        public List<ChatViewModel> GetUserChats(int userId)
        {
            var allChats = db.Chats.Include(ch => ch.Users).Include(ch => ch.Messages).ToList();
            List<Chat> userChats = new List<Chat>();
            foreach (var chat in allChats)
            {
                foreach (var user in chat.Users)
                {
                    if(user.Id == userId)
                    {
                        userChats.Add(chat);
                    }
                }
            }
            return MapChats(userChats);
        }

        private List<ChatViewModel> MapChats(List<Chat> userChats)
        {
            List<ChatViewModel> chatViews = new List<ChatViewModel>();
            foreach (var chat in userChats)
            {
                List<MessageViewModel> messageViews = new List<MessageViewModel>();
                foreach (var message in chat.Messages)
                {
                    messageViews.Add(new MessageViewModel
                    {
                        Id = message.Id,
                        Sender = message.Sender,
                        Text = message.Text
                    });
                }
                chatViews.Add(new ChatViewModel
                {
                    Id = chat.Id,
                    Name = chat.Name,
                    Messages = messageViews
                });
            }
            return chatViews;
        }
    }
}
