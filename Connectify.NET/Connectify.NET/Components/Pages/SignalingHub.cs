namespace Connectify.NET.Components.Pages
{
    using Microsoft.AspNetCore.SignalR;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SignalingHub : Hub
    {
        private static readonly Dictionary<string, List<string>> AudioRooms = new Dictionary<string, List<string>>();
        public async Task SendVideo(string roomId, byte[] videoData)
        {
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveVideo", videoData);
        }

        public async Task SendScreenShare(string roomId, byte[] screenData)
        {
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveScreenShare", screenData);
        }

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
            // Добавляем участника в указанную аудио-комнату
            if (AudioRooms.ContainsKey(roomId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                AudioRooms[roomId].Add(Context.ConnectionId); // Добавляем ConnectionId участника в список комнаты

                // Отправляем сообщение о присоединении нового участника другим участникам комнаты
                await Clients.OthersInGroup(roomId).SendAsync("UserJoined", Context.ConnectionId);
            }
        }

        public async Task SendAudio(string roomId, byte[] audioData)
        {
            // Отправляем аудио данным другим участникам комнаты
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveAudio", audioData);
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
