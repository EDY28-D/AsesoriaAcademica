using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

//Add advisory contract repository 

namespace CleanArchitecture.Infrastructure.Configurations
{
    public sealed class AdvisoryContractConfiguration : IEntityTypeConfiguration<AdvisoryContract>
    {
        public void Configure(EntityTypeBuilder<AdvisoryContract> builder)
        {
            //builder
            //    .Property(contract => contract.ThesisTopic)
            //    .IsRequired()
            //    .HasMaxLength(255);

            //builder
            //    .Property(contract => contract.Message)
            //    .IsRequired()
            //    .HasMaxLength(1000);
          
            

            //builder
            //    .Property(contract => contract.Status)
            //    .IsRequired()
            //    .HasMaxLength(20);

            //builder
            //    .HasOne(contract => contract.Professor)
            //    .WithMany()
            //    .HasForeignKey(contract => contract.ProfessorId);

            //builder
            //    .HasOne(contract => contract.Student)
            //    .WithMany()
            //    .HasForeignKey(contract => contract.StudentId);

            //builder
            //    .HasOne(contract => contract.ResearchLine)
            //    .WithMany()
            //    .HasForeignKey(contract => contract.ResearchLineId);

            // Seed data or other configurations can be added here
        }
    }
}