using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class ResearchGroupConfiguration : IEntityTypeConfiguration<ResearchGroup>
{
    public void Configure(EntityTypeBuilder<ResearchGroup> builder)
    {
        builder
            .Property(ResearchGroup =>ResearchGroup.Name)
            .IsRequired()
            .HasMaxLength(MaxLengths.ResearchGroup.Name);

        //builder.HasData(new ResearchGroup(
        //    Ids.Seed.ResearchGroupId,
        //    "INGENIERIA DE SOFTWARE","SW123"));
    }
}