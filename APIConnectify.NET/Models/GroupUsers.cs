namespace APIConnectify.NET.Models
{
    public class GroupUsers
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<Users> Participants { get; set; } = new List<Users>();


        public GroupUsers(int id, string groupName, List<Users> participants)
        {
            Id = id;
            GroupName = groupName;
            Participants = participants;
        }
        public GroupUsers()
        {

        }
    }
}
