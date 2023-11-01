using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyCollection.Domain.Entities;

namespace MyCollection.Data.Configuration
{
    internal class RentItemConfiguration : IEntityTypeConfiguration<RentItem>
    {
        public void Configure(EntityTypeBuilder<RentItem> builder)
        {
            builder.Property(r => r.RentedQuantity).IsRequired();
            builder.Property(r => r.QuantityReturned)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(r => r.RentDueDate).IsRequired();

            builder.HasOne(r => r.CollectionItem)
                .WithMany(item => item.Rentals)
                .HasForeignKey(r => r.CollectionItemId)
                .IsRequired();

            builder.HasOne(r => r.Borrower)
                .WithMany()
                .HasForeignKey(r => r.BorrowerId)
                .IsRequired();

            builder.Property(r => r.CreatedAt);
            builder.Property(r => r.UpdatedAt);
        }
    }
}
