using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIConnectify.NET.Models
{
    [Table("GroupsChats")]

    public class GroupsChats
    {
        [Key]

        public int Id { get; set; }
        [Required]
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
