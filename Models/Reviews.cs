using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_meet.Models
{
    public class Reviews
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Review { get ; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("user")]
        public int User_Id { get; set; }
        public User user { get; set; }

        [ForeignKey("Event")]
        public int Event_Id { get; set; }
        public Events Event { get; set; }

        public List<Comments> Comment { get; set; }
        public Reviews()
        {
            Comment = new List<Comments>();
        }
    }
}