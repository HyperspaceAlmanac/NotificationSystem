using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationSystem.Models;

namespace NotificationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Subscription>()
                .HasKey(subscription => new { subscription.UserId, subscription.SupervisorId });
                // For Composite primary key
        }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SupervisorToken> SupervisorTokens { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}
