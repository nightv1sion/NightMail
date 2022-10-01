using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MailFolder>().HasKey(mailFolder => new { mailFolder.MailId, mailFolder.FolderId });

            builder.Entity<Mail>()
                .HasOne<User>(m => m.Receiver)
                .WithMany(m => m.ReceivedMails)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Mail>()
                .HasOne<User>(m => m.Sender)
                .WithMany(u => u.SendedMails)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(u => u.ReceivedMails)
                .WithOne(m => m.Receiver)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(u => u.SendedMails)
                .WithOne(m => m.Sender)
                .OnDelete(DeleteBehavior.NoAction);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfileImage> UserProfileImages{ get; set; }
        public DbSet<Mail> Mails { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<MailFolder> MailFolders { get; set; }
    }
}
