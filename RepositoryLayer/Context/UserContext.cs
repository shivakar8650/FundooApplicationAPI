namespace RepositoryLayer.Context
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RepositoryLayer.Enitity;

    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<collaborator>()
                .HasOne(n => n.Note)
                .WithMany(u => u.collaborator)
                .HasForeignKey(ni => ni.NoteId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<collaborator>()
              .HasOne(u => u.User)
              .WithMany(u => u.collaborator)
              .HasForeignKey(ui => ui.UserId)
              .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<User> UserTable
        {
            get; set;
        }

        public DbSet<Note> NoteTable
        {
            get; set;
        }
        public DbSet<collaborator> collabTable
        {
            get; set;
        }
        public DbSet<Label> LabelsTable
        {
            get; set;
        }
    }
}
