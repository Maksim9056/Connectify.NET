﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIConnectify.NET.Models
{
    [Table("GroupsChats")]

    public class GroupsChats
    {
        //[Key]

        public int Id { get; set; }
        //[ForeignKey("GroupId")]
        public int Group { get; set; }

        public Users Users { get; set; }

        public string Messages { get; set; }

        public byte [] Bytes { get; set; }

        public GroupsChats(int id, int group, Users users, string messages, byte[] bytes)
        {
            Id = id;
            Group = group;
            Users = users;
            Messages = messages;
            Bytes = bytes;

        }
        public GroupsChats()
        {
        }
    }
}
