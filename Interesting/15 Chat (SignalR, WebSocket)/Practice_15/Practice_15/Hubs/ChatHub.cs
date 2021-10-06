using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Practice_15.Models;
using Microsoft.AspNetCore.Http;
using Practice_15.Data.DataServices.Interfaces;
using Practice_15.Data.Entity;

namespace Practice_15.Hubs
{
    public class ChatHub : Hub
    {
        IHttpContextAccessor _httpContextAccessor;
        string authUsername;
        IAccountsService accountsService;
        IChatService chatService;

        static List<ConnectedClient> connectedClients = new List<ConnectedClient>();
        static List<Group> activeGroups = new List<Group>();
        public ChatHub(IHttpContextAccessor httpContextAccessor, IAccountsService accountsService, IChatService chatService)
        {
            _httpContextAccessor = httpContextAccessor;
            // to get email of autorized user (autorized user = connected current user)
            authUsername = _httpContextAccessor.HttpContext.User.Identity.Name;
            this.accountsService = accountsService;
            this.chatService = chatService;
        }
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            // find and add new user to ConnectedClient list - list users that is online now
            User user = accountsService.FindUserByName(authUsername);
            ConnectedClient client = new ConnectedClient
            {
                ConnectionId = Context.ConnectionId,
                Id = user.Id,
                Username = user.Email,
            };
            connectedClients.Add(client);

            // send to new client his info and info of users that is online now
            await Clients.Caller.SendAsync("personalUserInfo", client);
            await Clients.Caller.SendAsync("usersList", connectedClients);
            // also his existed chats from DB
            var chats = chatService.GetUserChats(client.Id);
            foreach (var chat in chats)
            {
                // add chat to active groups. active groups - list of chats for work with them in ChatHub
                var srchChat = activeGroups.FirstOrDefault(g => g.Id == chat.Id);
                if(srchChat is null)
                {
                    activeGroups.Add(new Group { Id = chat.Id, Name = chat.Name });
                }
                // add current user to all his chats (that was in DB)
                await Groups.AddToGroupAsync(Context.ConnectionId, chat.Name);
            }
            // send for current user all his chats (that was in DB)
            await Clients.Caller.SendAsync("chatsList", chats);

            
            // add new user to other users in online users list
            await Clients.Others.SendAsync("newUserConnected", client);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            ConnectedClient client = connectedClients.FirstOrDefault(c => c.ConnectionId == Context.ConnectionId);
            var chats = chatService.GetUserChats(client.Id);
            foreach (var chat in chats)
            {
                // remove current user from all his chats (signalR)
                await Groups.RemoveFromGroupAsync(client.ConnectionId, chat.Name);
            }
            // refresh the list for other users in the UI
            connectedClients.Remove(client);
            await Clients.Others.SendAsync("usersList", connectedClients);
        }

        public async Task GetChatMessages(int chatId)
        {
            // by chat selection in chats list (in UI) send all this chat messages from DB
            List<ChatMessage> messages = chatService.GetChatMessages(chatId);
            await Clients.Caller.SendAsync("receiveChatMessages", messages);

        }

        public async Task SendMessage(ChatMessage message)
        {
            string groupName = activeGroups.First(g => g.Id == message.ChatId).Name;
            await Clients.Group(groupName).SendAsync("receiveMessage", message);

            // save this message in DB
            chatService.AddNewMessageInDB(message);
        }


        public async Task AddNewChat(ConnectedClient creator, int invitedUserId, string groupName)
        {
            // save chat in DB
            chatService.CreateNewChat(creator.Id, invitedUserId, groupName);

            // add chat participants to signalR group (by ConnectionId)
            string invitedUserConnectionId = connectedClients.First(c => c.Id == invitedUserId).ConnectionId;// user exists for shure
            await Groups.AddToGroupAsync(creator.ConnectionId, groupName);
            await Groups.AddToGroupAsync(invitedUserConnectionId, groupName);

            // add new chat to users UI
            ChatViewModel chatView = chatService.GetNewChatFromDb();
            // add to active groups
            activeGroups.Add(new Group { Id = chatView.Id, Name = chatView.Name });

            await Clients.Group(groupName).SendAsync("addNewChat", chatView);
        }
    }
}
