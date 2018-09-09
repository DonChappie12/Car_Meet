using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_meet.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }

        public string Comment { get; set; }

        [ForeignKey("user")]
        public int User_Id { get; set; }
        public User user { get; set; }

        [ForeignKey("Review")]
        public int Reviews_Id { get; set; }
        public Reviews Review { get; set; }
    }
}