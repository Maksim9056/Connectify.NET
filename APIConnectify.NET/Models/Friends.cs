namespace APIConnectify.NET.Models
{
    public class Friends
    {
        public int Id { get; set; }
        public Users Friend { get; set; } 
        public Users UserTo { get; set; }

        public Friends(int id, Users friend, Users userTo)
        {
            Id = id;
            Friend = friend;
            UserTo = userTo;
        }
        public Friends()
        {

        }
    }
}
