using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public sealed class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            //builder
            //    .Property(appointment => appointment.DateTime)
            //    .IsRequired();

            //builder
            //    .Property(appointment => appointment.ProfessorProgress)
            //    .HasMaxLength(1000);

            //builder
            //    .Property(appointment => appointment.StudentProgress)
            //    .HasMaxLength(1000);

            //builder
            //    .HasOne(appointment => appointment.Professor)
            //    .WithMany()
            //    .HasForeignKey(appointment => appointment.ProfessorId);

            //builder
            //    .HasOne(appointment => appointment.Student)
            //    .WithMany()
            //    .HasForeignKey(appointment => appointment.StudentId);

            // Seed data or other configurations can be added here
        }
    }
}
