using Microsoft.EntityFrameworkCore;
using SimpleChatApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.DAL
{
    public class SimpleChatDbContext:DbContext
    {
        public SimpleChatDbContext(DbContextOptions<SimpleChatDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserInChat> UsersInChat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
