using System.ComponentModel.DataAnnotations.Schema;

namespace APIConnectify.NET.Models
{
    [Table("Files")]

    public class Files
    {
        public  int Id { get; set; }

        public byte [] Name { get; set; }

        public Files(int id, byte[] name)
        {
            Id = id;
            Name = name;
        }
    }
}
