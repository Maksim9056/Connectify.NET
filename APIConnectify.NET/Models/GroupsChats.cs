namespace APIConnectify.NET.Models
{
    public class GroupsChats
    {
        public int Id { get; set; }
        public Group Group { get; set; }

        public string Messages { get; set; }
        public Files Files { get; set; }

        public GroupsChats(int id, Group group, string messages, Files files)
        {
            Id = id;
            Group = group;
            Messages = messages;
            Files = files;
        }
        public GroupsChats()
        {

        }
    }
}
