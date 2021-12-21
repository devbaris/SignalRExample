using Microsoft.AspNetCore.SignalR;
using WebAPI.DTOs;

namespace WebAPI
{
    public class ChatHub : Hub
    {
        public void SendMessage(MessageDto messageDto)
        {
            Clients.Others.SendAsync("receiveMessage", messageDto);
        }
    }
}