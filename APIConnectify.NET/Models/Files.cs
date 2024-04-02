namespace APIConnectify.NET.Models
{
    public class Files
    {
        public Files()
        {

        }
        public  int Id { get; set; }

        public byte [] Name { get; set; }

        public Files(int id, byte[] name)
        {
            Id = id;
            Name = name;
        }
    }
}
