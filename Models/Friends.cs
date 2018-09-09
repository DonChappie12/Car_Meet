using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_meet.Models
{
    public class Friends
    {
        // [Key]
        public int FriendsId { get; set; }

        [ForeignKey("Request")]
        public int Friend_Id { get; set; }
        public User Request { get; set; }

        [ForeignKey("Requester")]
        public int User_Id { get; set; }
        public User Requester { get; set; }
    }
}