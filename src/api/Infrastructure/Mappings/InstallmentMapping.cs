using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infrastructure.Mappings
{
    public class InstallmentMapping : IEntityTypeConfiguration<Installment>
    {
        public void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => i.Key).IsUnique();

            builder.ToTable("Installments");
        }
    }
}