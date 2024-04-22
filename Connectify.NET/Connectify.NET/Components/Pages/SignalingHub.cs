namespace Connectify.NET.Components.Pages
{
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.JSInterop;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SignalingHub : Hub
    {
        private static readonly Dictionary<string, List<string>> AudioRooms = new Dictionary<string, List<string>>();

        public async Task CreateAudioRoom(string roomId)
        {
            // Создаем новую аудио-комнату, если она еще не существует
            if (!AudioRooms.ContainsKey(roomId))
            {
                AudioRooms.Add(roomId, new List<string>());
            }
        }
        public async Task JoinAudioRoom(string roomId)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                throw new ArgumentException("Room ID cannot be empty or null.", nameof(roomId));
            }

            if (AudioRooms.ContainsKey(roomId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                AudioRooms[roomId].Add(Context.ConnectionId);

                await Clients.OthersInGroup(roomId).SendAsync("UserJoined", Context.ConnectionId);

                var activeUsersInRoom = AudioRooms[roomId];
                await Clients.Caller.SendAsync("ActiveUsersInRoom", activeUsersInRoom);
            }
            else
            {
                throw new ArgumentException($"Room '{roomId}' does not exist.", nameof(roomId));
            }
        }


        public async Task LeaveAudioRoom(string roomId)
        {
            // Удаляем участника из указанной аудио-комнаты
            if (AudioRooms.ContainsKey(roomId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
                AudioRooms[roomId].Remove(Context.ConnectionId); // Удаляем ConnectionId участника из списка комнаты

                // Отправляем сообщение об отсоединении участника другим участникам комнаты
                await Clients.OthersInGroup(roomId).SendAsync("UserLeft", Context.ConnectionId);
            }
        }

        [JSInvokable]
        public async Task SendAudio(string roomId, byte[] audioData)
        {
            if (AudioRooms.ContainsKey(roomId))
            {
                var senderId = Context.ConnectionId;
                var receiverIds = AudioRooms[roomId].Where(id => id != senderId).ToList();

                if (receiverIds.Any())
                {
                    await Clients.Clients(receiverIds).SendAsync("ReceiveAudio", senderId, audioData);
                }
            }
        }

        public async Task StartCall(string roomId)
        {
            // Отправить сигнал о начале звонка другим участникам
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveCallStart");
        }

        public async Task EndCall(string roomId)
        {
            // Отправить сигнал об окончании звонка другим участникам
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveCallEnd");
        }
    }




}
