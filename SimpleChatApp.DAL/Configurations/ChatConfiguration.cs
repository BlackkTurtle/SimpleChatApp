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
    public class ChatConfiguration: IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasOne(mg => mg.User)
                .WithMany(m => m.Chats)
                .HasForeignKey(mg => mg.CreatorId);

            builder.HasMany(a => a.Messages)
                .WithOne(a=>a.Chat)
                .HasForeignKey(ma => ma.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.UserInChats)
                .WithOne(a => a.Chat)
                .HasForeignKey(ma => ma.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
