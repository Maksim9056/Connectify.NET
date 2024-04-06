﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIConnectify.NET.Models
{
    [Table("Groups")]
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
        public List<Users> Participants { get; set;} = new List<Users>();
     
        public Group(int id, string groupName, List<Users> participants)
        {
            Id = id;
            GroupName = groupName;
            Participants = participants;
        }
        public Group()
        {

        }
    }
}
