namespace Connectify.NET.Components.Pages
{
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.JSInterop;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SignalingHub : Hub
    {
        private static readonly ConcurrentDictionary<string, List<string>> AudioRooms = new ConcurrentDictionary<string, List<string>>();

        public async Task CreateAudioRoom(string roomId)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                throw new ArgumentException("Room ID cannot be empty or null.", nameof(roomId));
            }

            AudioRooms.TryAdd(roomId, new List<string>());
        }

        public async Task JoinAudioRoom(string roomId)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                throw new ArgumentException("Room ID cannot be empty or null.", nameof(roomId));
            }

            if (AudioRooms.TryGetValue(roomId, out var users))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                users.Add(Context.ConnectionId);

                await Clients.OthersInGroup(roomId).SendAsync("UserJoined", Context.ConnectionId);

                await Clients.Caller.SendAsync("ActiveUsersInRoom", users);
            }
            else
            {
                throw new ArgumentException($"Room '{roomId}' does not exist.", nameof(roomId));
            }
        }

        public async Task LeaveAudioRoom(string roomId)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                throw new ArgumentException("Room ID cannot be empty or null.", nameof(roomId));
            }

            if (AudioRooms.TryGetValue(roomId, out var users) && users.Remove(Context.ConnectionId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);

                await Clients.OthersInGroup(roomId).SendAsync("UserLeft", Context.ConnectionId);

                if (users.Count == 0)
                {
                    AudioRooms.TryRemove(roomId, out _); // Remove room if no users left
                }
            }
        }

        public async Task SendAudio(string roomId, byte[] audioData)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                throw new ArgumentException("Room ID cannot be empty or null.", nameof(roomId));
            }

            if (AudioRooms.TryGetValue(roomId, out var users))
            {
                var senderId = Context.ConnectionId;
                var receiverIds = users.Where(id => id != senderId).ToList();

                if (receiverIds.Any())
                {
                    await Clients.Clients(receiverIds).SendAsync("ReceiveAudio", senderId, audioData);
                }
            }
        }
        public async Task SendAudioToCaller(string roomId, byte[] audioData)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                throw new ArgumentException("Room ID cannot be empty or null.", nameof(roomId));
            }

            if (AudioRooms.ContainsKey(roomId))
            {
                var senderId = Context.ConnectionId;
                await Clients.Client(senderId).SendAsync("ReceiveAudio", senderId, audioData);
            }
        }

        public async Task StartCall(string roomId)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                throw new ArgumentException("Room ID cannot be empty or null.", nameof(roomId));
            }

            // Отправить сигнал о начале звонка другим участникам
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveCallStart");
        }

        public async Task EndCall(string roomId)
        {
            if (string.IsNullOrEmpty(roomId))
            {
                throw new ArgumentException("Room ID cannot be empty or null.", nameof(roomId));
            }

            // Отправить сигнал об окончании звонка другим участникам
            await Clients.OthersInGroup(roomId).SendAsync("ReceiveCallEnd");
        }
    }
}
