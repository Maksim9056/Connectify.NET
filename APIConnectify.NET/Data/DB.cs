using APIConnectify.NET.Models;
using Microsoft.EntityFrameworkCore;
//using Npgsql.EntityFrameworkCore.PostgreSQL;
//using APIConnectify.NET.Models;
namespace APIConnectify.NET.Data
{
    public class DB:DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<GroupsChats> GroupsChats { get; set; } = null!;
        public DbSet<Group> Group { get; set; } = null!;

        public DbSet<Friends> Friends { get; set; } = null!;
        public DbSet<Files> Files { get; set; } = null!;
    }

    public class Settings
    {
        public int KEYSQL { get; set; }
        public string APIURL { get; set; }
        public string APIKey { get; set; } // API ключ для аутентификации

        // Конструктор для инициализации значений KEYSQL, APIURL и APIKey
        public Settings(int keysql, string apiurl, string apiKey)
        {
            KEYSQL = keysql;
            APIURL = apiurl;
            APIKey = apiKey;
        }


    }
}

//public DB()
//{
//    Database.EnsureCreated();
//}
//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TestdbS;Username=postgres;Password=1");

//    // Укажите здесь строку подключения к вашей базе данных
//    //optionsBuilder.UseSqlServer("Server=МАКСИМ;Database=master;Trusted_Connection=True;Integrated Security=True;");
//}
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Friends>()
//.HasKey(f => new { f.UserTo.Id, f.Friend }); // Предполагается, что у вас составной ключ для друзей

//        modelBuilder.Entity<Friends>()
//            .HasOne(f => f.UserTo)
//            .WithMany(u => u.Friends)
//            .HasForeignKey(f => f.UserTo.Id)
//            .OnDelete(DeleteBehavior.Restrict); // Если необходимо, укажите тип удаления

//        modelBuilder.Entity<Friends>()
//            .HasOne(f => f.Friend)
//            .WithMany()
//            .HasForeignKey(f => f.Friend.Id)
//            .OnDelete(DeleteBehavior.Restrict); // Если необходимо, укажите тип удаления

//        modelBuilder.Entity<Group>()
//            .HasKey(g => g.Id);

//        modelBuilder.Entity<Group>()
// .HasMany(g => g.Participants)
// .WithMany(u => u.Group);


//        modelBuilder.Entity<GroupsChats>()
//            .HasKey(gc => gc.Id);

//        modelBuilder.Entity<GroupsChats>()
//            .HasOne(gc => gc.Group)
//            .WithMany()
//            .HasForeignKey(gc => gc.Group.Id);

//        modelBuilder.Entity<Files>()
//            .HasKey(f => f.Id);
//    }