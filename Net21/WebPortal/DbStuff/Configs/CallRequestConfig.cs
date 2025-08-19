using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebPortal.DbStuff.Models;

namespace WebPortal.DbStuff.Configs.Cdek;

public sealed class CallRequestConfig : IEntityTypeConfiguration<CallRequest>
{
    public void Configure(EntityTypeBuilder<CallRequest> builder)
    {
        builder.ToTable("CallRequests");
        
        builder.HasIndex(x => x.Id).IsUnique();
        
        builder.Property(x => x.Id).HasColumnName(@"Id").IsRequired().UseIdentityColumn();
        builder.Property(x => x.Name).HasColumnName(@"Name").IsRequired();
        builder.Property(x => x.Question).HasColumnName(@"Question");
        builder.Property(x => x.PhoneNumber).HasColumnName(@"PhoneNumber").IsRequired();
        builder.Property(x => x.CreationTime).HasColumnName(@"CreationTime").IsRequired();
    }
}