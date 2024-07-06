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
    public class MessageConfiguration: IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Created)
                .IsRequired();

            builder.Property(a => a.Updated)
                .IsRequired();

            builder.Property(a => a.Text)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(mg => mg.User)
                .WithMany(m => m.Messages)
                .HasForeignKey(mg => mg.UserId);

            builder.HasOne(mg => mg.Chat)
                .WithMany(m => m.Messages)
                .HasForeignKey(mg => mg.ChatId);
        }
    }
}
