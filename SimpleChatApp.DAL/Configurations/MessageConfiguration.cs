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
    public class UserInChatConfiguration: IEntityTypeConfiguration<UserInChat>
    {
        public void Configure(EntityTypeBuilder<UserInChat> builder)
        {
            builder.HasKey(a => new {a.UserId,a.ChatId});

            builder.HasOne(mg => mg.User)
                .WithMany(m => m.UserInChats)
                .HasForeignKey(mg => mg.UserId);

            builder.HasOne(mg => mg.Chat)
                .WithMany(m => m.UserInChats)
                .HasForeignKey(mg => mg.ChatId);
        }
    }
}
