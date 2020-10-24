using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public class CardTransactionMapping : IEntityTypeConfiguration<CardTransaction>
    {
        public void Configure(EntityTypeBuilder<CardTransaction> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.CardFinal)
                .HasMaxLength(4);

            builder.HasMany(t => t.Installments)
                .WithOne(i => i.Transaction)
                .HasForeignKey(i => i.TransactionId);

            builder.ToTable("CardTransactions");
        }
    }
}