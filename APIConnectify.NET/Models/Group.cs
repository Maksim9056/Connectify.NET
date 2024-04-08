using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIConnectify.NET.Models
{
    [Table("Group")]
    public class Group
    {
        [Key]

        public int Id { get; set; }
        public string GroupName { get; set; }
        //public List<int> Participants { get; set;} = new List<int>();
        public List<int> Participants { get; set; } = new List<int>();


        //public Group(int id, string groupName, List<int> participants)
        //{
        //    Id = id;
        //    GroupName = groupName;
        //    Participants = participants;
        //}
        //public Group()
        //{

        //}
    }
}
