using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Enitity;

namespace RepositoryLayer.Context
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> UserTable
        {
            get; set;
        }

        public DbSet<Note> NoteTable
        {
            get; set;
        }

        public DbSet<collaborator> collaborationTable
        {
            get; set;
        }
    }

}
