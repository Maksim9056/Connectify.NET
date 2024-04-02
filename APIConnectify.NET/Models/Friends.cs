namespace APIConnectify.NET.Models
{
    public class Friends
    {
        public int Id { get; set; }
        public int  Friend { get; set; } 
        public int UserTo { get; set; }

        public Friends(int id, int friend, int userTo)
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
