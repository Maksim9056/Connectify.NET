namespace APIConnectify.NET.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public Files Picture { get; set; }
        public List<int> Group { get; set; } = new List<int>();

        public List<int> Friends { get; set; } = new List<int>();
        public Users() { }
        public Users(int id, string username, string password, string email, string surname, string phone, Files picture, List<int> group, List<int> friends)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Surname = surname;
            Phone = phone;
            Picture = picture;
            Group = group;
            Friends = friends;
        }
    }
}
