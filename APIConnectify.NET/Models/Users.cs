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
        public List<Group> Group { get; set; } = new List<Group>();
        public List<Friends> Friends { get; set; } = new List<Friends>();
        public Users(int id, string username, string password, string email, string surname, string phone, Files picture, List<Group> group, List<Friends> friends)
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
