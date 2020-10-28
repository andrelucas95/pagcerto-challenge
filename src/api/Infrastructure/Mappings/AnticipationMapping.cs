using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public class AnticipationMapping : IEntityTypeConfiguration<Anticipation>
    {
        public void Configure(EntityTypeBuilder<Anticipation> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasMany(a => a.CardTransactions);

            builder.ToTable("Anticipations");
        }
    }
}