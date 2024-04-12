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
        public int  UserId { get; set; }
        public int Friend { get; set; }

        public Friends()
        {

        }
        public Friends(int id, int userId, int friend)
        {
            Id = id;
            UserId = userId;
            Friend = friend;
        }
     
    }
}
