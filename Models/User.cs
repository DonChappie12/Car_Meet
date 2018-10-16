using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace car_meet.Models
{
    public class User
    {
        
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string AboutMe { get; set; }
        public string WhatILike { get; set; }
        public string MyCars { get; set; }

        public List<Events> Event { get; set; }

        public List<Going> Going { get; set; }
        public List<Reviews> Review { get ; set; }
        public List<Comments> Comment { get; set; }

        [InverseProperty("Requester")]
        public List<Friends> Request { get; set; }

        [InverseProperty("Request")]
        public List<Friends> Requester { get; set; }
        public User()
        {
            Event = new List<Events>();
            Going = new List<Going>();
            Review = new List<Reviews>();
            Comment = new List<Comments>();
            Request = new List<Friends>();
            Requester = new List<Friends>();
        }
    }
}