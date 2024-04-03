using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIConnectify.NET.Models
{
    [Table("Friends")]
    public class Friends
    {
        [Key]

        public int Id { get; set; }
        //[Required]
        public Users FriendId { get; set; }

        public Friends()
        {

        }
        public Friends(int id, Users friendId)
        {
            Id = id;
            FriendId = friendId;
        }
     
    }
}
