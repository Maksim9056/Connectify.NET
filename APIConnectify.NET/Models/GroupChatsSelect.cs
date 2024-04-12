namespace APIConnectify.NET.Models
{
    public class GroupChatsSelect
    {
        public int Id { get; set; }
        public GroupUsers Group { get; set; }
        public Users Users { get; set; }
        public string Messages { get; set; }
        public byte[] Bytes { get; set; }

        public GroupChatsSelect(int id, GroupUsers group, Users users, string messages, byte[] bytes)
        {
            Id = id;
            Group = group;
            Users = users;
            Messages = messages;
            Bytes = bytes;
        }

        public GroupChatsSelect()
        {

        }
    }
}
