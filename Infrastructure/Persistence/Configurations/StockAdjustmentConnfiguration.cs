using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StockAdjustmentConnfiguration : IEntityTypeConfiguration<StockAdjustment>
{
    public void Configure(EntityTypeBuilder<StockAdjustment> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Reason)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.AdjustmentAmount)
            .IsRequired();

        builder.Property(x => x.AdjustmentDate)
            .IsRequired();

        builder.HasOne(x => x.Product)
            .WithMany(x => x.StockAdjustments)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}