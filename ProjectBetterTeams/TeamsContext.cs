namespace ProjectBetterTeams
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TeamsContext : DbContext
    {
        public TeamsContext()
            : base("name=TeamsContext")
        {
        }

        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Messages>()
                .Property(e => e.UsernameSender)
                .IsUnicode(false);

            modelBuilder.Entity<Messages>()
                .Property(e => e.Receiver)
                .IsUnicode(false);

            modelBuilder.Entity<Messages>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Posts>()
                .Property(e => e.UsernameSender)
                .IsUnicode(false);

            modelBuilder.Entity<Posts>()
                .Property(e => e.Post)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.UserType)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UsernameSender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UsernameSender)
                .WillCascadeOnDelete(false);
        }
    }
}
