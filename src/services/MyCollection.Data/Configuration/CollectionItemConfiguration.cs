using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCollection.Domain.Entities;
namespace MyCollection.Data.Configuration
{
    public class CollectionItemConfiguration : IEntityTypeConfiguration<CollectionItem>
    {
        public void Configure(EntityTypeBuilder<CollectionItem> builder)
        {
            builder.HasKey(item => item.Id);

            builder.Property(item => item.Title)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(item => item.Autor)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(item => item.Quantity)
                .HasDefaultValue(1)
                .IsRequired();

            builder.Property(item => item.Edition);

            builder.Property(item => item.ItemType)
                .IsRequired();

            builder.Property(item => item.Status)
                .HasDefaultValue(ECollectionStatus.AVAILABLE)
                .IsRequired();

            builder.HasOne(item => item.Location)
                .WithMany(location => location.CollectionItems)
                .HasForeignKey(item => item.LocationId);

            builder.HasMany(item => item.Borrowers)
                .WithOne()
                .HasForeignKey(item => item.Id);
        }
    }
}
