using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIConnectify.NET.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]

        public int Id { get; set; }
        
        public string Username { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public Files Picture { get; set; }
        public List<int> Group { get; set; } = new List<int>();
        public List<int> Friends { get; set; } = new List<int>();
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
        public Users()
        {
        }
    }
}
