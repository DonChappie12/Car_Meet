using System;
using Microsoft.EntityFrameworkCore;

namespace car_meet.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Going> Going { get; set; }
        public DbSet<Events> Event { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Friends> Friends { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    }
}