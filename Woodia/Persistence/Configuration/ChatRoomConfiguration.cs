using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Woodia.Entity;

namespace Woodia.Persistence.Configuration;

public class ChatRoomConfiguration:IEntityTypeConfiguration<ChatRoom>
{
    public void Configure(EntityTypeBuilder<ChatRoom> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasOne(x=>x.Client)
            .WithMany(x=>x.ClientChatRooms)
            .HasForeignKey(x=>x.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x=>x.Carpenter)
            .WithMany(x=>x.CarpenterChatRooms)
            .HasForeignKey(x=>x.CarpenterId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x=>x.Offer)
            .WithMany(x=>x.ChatRooms)
            .HasForeignKey(x=>x.OfferId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(x => x.Messages)
            .WithOne(x => x.ChatRoom)
            .HasForeignKey(x => x.ChatRoomId);
    }
}