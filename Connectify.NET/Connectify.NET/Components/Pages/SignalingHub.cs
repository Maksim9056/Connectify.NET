namespace Connectify.NET.Components.Pages
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class SignalingHub : Hub
    {
        public async Task JoinAudioRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.OthersInGroup(roomId).SendAsync("UserJoined", Context.ConnectionId);
        }

        public async Task SendAudio(string roomId, byte[] audioData)
        {
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveAudio", audioData);
        }

        public async Task LeaveAudioRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.OthersInGroup(roomId).SendAsync("UserLeft", Context.ConnectionId);
        }

        public async Task StartCall()
        {
            // Отправить сигнал о начале звонка другим участникам
            await Clients.Others.SendAsync("ReceiveCallStart");
        }

        public async Task EndCall()
        {
            // Отправить сигнал об окончании звонка другим участникам
            await Clients.Others.SendAsync("ReceiveCallEnd");
        }
    }


}
