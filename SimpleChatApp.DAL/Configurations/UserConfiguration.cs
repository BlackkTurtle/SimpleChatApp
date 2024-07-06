using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleChatApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChatApp.DAL.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.UserName)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasMany(a => a.Messages)
                .WithOne(a=>a.User)
                .HasForeignKey(ma => ma.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.UserInChats)
                .WithOne(a => a.User)
                .HasForeignKey(ma => ma.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.Chats)
                .WithOne(a => a.User)
                .HasForeignKey(ma => ma.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
