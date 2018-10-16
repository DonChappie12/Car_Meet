using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_meet.Models
{
    public class Events
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="An Event must have a Title")]
        [MinLength(2, ErrorMessage="Event must have two or more characters")]
        public string Title { get; set; }

        [Required(ErrorMessage="An Event must have a location to be hosted at")]
        public string City { get; set; }

        [Required(ErrorMessage="An Event must have a Time")]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage="Must clarify how long this Event is going to last")]
        public string TimeAmt { get; set; }

        [Required(ErrorMessage="An Event must have an address for car meeters")]
        public string Adress { get; set; }

        [Required(ErrorMessage="An Event must have a Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage="An event must have a Description")]
        public string Description { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Created_At { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Updated_At { get; set; }

        [ForeignKey("user")]
        public int User_Id { get; set; }
        public User user { get; set; }

        public List<Going> Going { get; set;}

        public List<Reviews> Review { get; set; }

        public Events()
        {
            Going = new List<Going>();
            Review = new List<Reviews>();
        }
    }
}