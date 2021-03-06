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
                .HasKey(subscription => new { subscription.PublisherId, subscription.SubscriberId });
            builder.Entity<Subscription>()
                .HasOne(typeof(User), "Publisher")
                .WithMany()
                .HasForeignKey("PublisherId")
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Subscription>()
                .HasOne(typeof(User), "Subscriber")
                .WithMany()
                .HasForeignKey("SubscriberId")
                .OnDelete(DeleteBehavior.Restrict);
            // For Composite primary key
        }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
    }
}
