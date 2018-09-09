using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_meet.Models
{
    public class Going
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("user")]
        public int User_Id { get; set; }
        public User user { get; set; }

        [ForeignKey("events")]
        public int Event_Id { get; set; }
        public Events events { get; set; }
    }
}